using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MissedPart : MonoBehaviour
{
	Renderer renderer;

	public void Init(Vector3 position, Vector3 scale, Material material,MaterialPropertyBlock propertyBlock)
	{
		// 포지션과 스케일 설정
		transform.position = position;
		transform.localScale = scale;

		// 랜더러가 한번 설정되면 재사용 되기 때문에 null 체크
		if (!renderer)
		{
			TryGetComponent(out renderer);
		}
		// 머티리얼 프로퍼티 블록 설정
		renderer.SetPropertyBlock(propertyBlock);

		gameObject.SetActive(true);

		Invoke(nameof(Disable), 5f);
	}
	private void Disable()
	{
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.ReturnToPool(this);
	}
}