using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MissedPart : MonoBehaviour
{
	Renderer renderer;
	Rigidbody rb;

	public void Init(Vector3 position, Vector3 scale, Material material,MaterialPropertyBlock propertyBlock)
	{
		// 트랜스폼 설정
		transform.SetPositionAndRotation(position, Quaternion.identity);
		transform.localScale = scale;

		// 랜더러가 한번 설정되면 재사용 되기 때문에 null 체크
		if (!renderer)
		{
			TryGetComponent(out renderer);
		}
		// 머티리얼 프로퍼티 블록 설정
		renderer.SetPropertyBlock(propertyBlock);

		// 리지드바디 초기화
		if (!rb)
		{
			TryGetComponent(out rb);
		}
		// 초기 속도 및 각속도 초기화
		rb.linearVelocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		gameObject.SetActive(true);

		Invoke(nameof(Disable), 5f);
	}
	private void Disable()
	{
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.ReturnToPool(this);
	}
}