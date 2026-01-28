using UnityEngine;

public enum SFXType
{
	Start,
	Hit,
	Perfect,
	Miss,
	GameOver
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	// 싱글톤 인스턴스
	public static AudioManager Instance { get; private set; }

	// 오디오 소스 및 클립들
	AudioSource audioSource;
	[SerializeField] AudioClip startClip;
	[SerializeField] AudioClip hitClip;
	// 퍼펙트 클립들
	[SerializeField] AudioClip[] perfectClips;
	// 현재 퍼펙트 클립 인덱스
	int perfectClipIndex = 0;
	[SerializeField] AudioClip missClip;
	[SerializeField] AudioClip gameOverClip;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		TryGetComponent(out audioSource);
	}

	public void PlaySFX(SFXType type)
	{
		switch (type)
		{
			case SFXType.Start:
				audioSource.PlayOneShot(startClip);
				break;
			case SFXType.Hit:
				audioSource.PlayOneShot(hitClip);
				// 퍼펙트 클립 인덱스 초기화
				perfectClipIndex = 0;
				break;
			case SFXType.Perfect:
				audioSource.PlayOneShot(perfectClips[perfectClipIndex]);
				// 퍼펙트 클립 인덱스 증가 (최대값 제한)
				perfectClipIndex = Mathf.Min(perfectClipIndex + 1, perfectClips.Length - 1);
				break;
			case SFXType.Miss:
				audioSource.PlayOneShot(missClip);
				perfectClipIndex = 0;
				break;
			case SFXType.GameOver:
				audioSource.PlayOneShot(gameOverClip);
				break;
			default:
				break;
		}
	}
}
