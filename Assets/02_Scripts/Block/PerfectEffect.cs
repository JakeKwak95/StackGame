using UnityEngine;

public class PerfectEffect : MonoBehaviour
{
	[SerializeField] float duration = 0.5f;
	[SerializeField] float growAmount = .25f;

	public void Init()
	{
		// 포지션 XZ는 월드 오리진
		transform.position = UtilClass.WorldOrigin;
		//  Y는 큐브 높이의 절반민큼 올리기
		// 큐브 높이의 절반민큼 올리는 이유는 다음 블록과 현재 블록 사이에 이펙트가 위치해야 하기 때문
		transform.position += Vector3.up * (UtilClass.CubeHeight / 2);
		// 스케일은 XZ는 큐브 스케일, Y는 매우 얇게
		Vector3 scale = UtilClass.CubeScale;
		scale.y = .001f;
		transform.localScale = scale;

		gameObject.SetActive(true);
		Invoke(nameof(Disable), duration);
	}

	private void Update()
	{
		// XZ축으로만 점점 커지기
		Vector3 xz = UtilClass.CubeScale;
		xz.y = 0f;
		// 스케일 증가량을 프레임 단위로 적용
		transform.localScale += growAmount * Time.deltaTime * xz;
	}

	private void Disable()
	{
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.ReturnToPool(this);
	}
}
