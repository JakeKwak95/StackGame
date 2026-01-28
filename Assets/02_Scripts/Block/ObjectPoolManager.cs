using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀링 타입
public enum PoolingType
{
	MissedPart,
	PerfectEffect,
}

public class ObjectPoolManager : MonoBehaviour
{
	public static ObjectPoolManager Instance { get; private set; }

	[SerializeField] int poolSize = 5;
	[SerializeField] MissedPart missedPartPrefab;
	Queue<MissedPart> missedPartPool = new Queue<MissedPart>();
	// 퍼펙트 이펙트 프리팹과 풀
	[SerializeField] PerfectEffect perfectEffectPrefab;
	Queue<PerfectEffect> effectPool = new Queue<PerfectEffect>();

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

		MakePool(PoolingType.MissedPart);
		MakePool(PoolingType.PerfectEffect);
	}

	private void MakePool(PoolingType poolingType)
	{
		switch (poolingType)
		{
			case PoolingType.MissedPart:
				for (int i = 0; i < poolSize; i++)
				{
					MissedPart missedPart = Instantiate(missedPartPrefab, transform);
					missedPartPool.Enqueue(missedPart);
					missedPart.gameObject.SetActive(false);
				}
				break;

			// 퍼펙트 이펙트 풀 생성
			case PoolingType.PerfectEffect:
				for (int i = 0; i < poolSize; i++)
				{
					PerfectEffect effect = Instantiate(perfectEffectPrefab, transform);
					effectPool.Enqueue(effect);
					effect.gameObject.SetActive(false);
				}
				break;
			default:
				break;
		}
	}

	public MissedPart GetMissedPart()
	{
		if (missedPartPool.Count > 0)
		{
			MissedPart missedPart = missedPartPool.Dequeue();
			return missedPart;
		}
		else
		{
			MakePool(PoolingType.MissedPart);
			return GetMissedPart();
		}
	}

	// 퍼펙트 이펙트 가져오기
	public PerfectEffect GetPerfectEffect()
	{
		if (effectPool.Count > 0)
		{
			PerfectEffect effect = effectPool.Dequeue();
			return effect;
		}
		else
		{
			MakePool(PoolingType.PerfectEffect);
			return GetPerfectEffect();
		}
	}

	public void ReturnToPool(MissedPart missedPart)
	{
		missedPartPool.Enqueue(missedPart);
	}

	// 퍼펙트 이펙트 반환(매개변수를 변경하여 오버로딩)
	public void ReturnToPool(PerfectEffect effect)
	{
		effectPool.Enqueue(effect);
	}
}
