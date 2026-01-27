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
			missedPart.gameObject.SetActive(true);
			return missedPart;
		}
		else
		{
			MakePool(PoolingType.MissedPart);
			return GetMissedPart();
		}
	}

	public PerfectEffect GetPerfectEffect()
	{
		if (effectPool.Count > 0)
		{
			PerfectEffect effect = effectPool.Dequeue();
			effect.gameObject.SetActive(true);
			effect.Init();
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

	public void ReturnToPool(PerfectEffect effect)
	{
		effectPool.Enqueue(effect);
	}
}
