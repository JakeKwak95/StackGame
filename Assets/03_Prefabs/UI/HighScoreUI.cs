using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI highScoreText;

	private void Awake()
	{
		highScoreText.gameObject.SetActive(false);
	}

	// 구독 및 구독 해제
	private void OnEnable()
	{
		GameManager.OnGameOver += UpdateHighScore;
	}
	private void OnDisable()
	{
		GameManager.OnGameOver -= UpdateHighScore;
	}

	private void UpdateHighScore()
	{
		// 현재 점수가 저장된 최고 점수보다 높으면 저장
		if (GameManager.Instance.Score > SaveManager.Instance.LoadScore())
		{
			SaveManager.Instance.SaveScore(GameManager.Instance.Score);
		}

		// 텍스트 업데이트
		// \n : 줄바꿈
		string label = "High Score\n";
		highScoreText.SetText(label + SaveManager.Instance.LoadScore().ToString());

		highScoreText.gameObject.SetActive(true);
	}
}
