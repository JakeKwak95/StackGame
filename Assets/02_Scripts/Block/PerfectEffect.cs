using UnityEngine;

public class PerfectEffect : MonoBehaviour
{
	[SerializeField] float duration = 0.2f;
	[SerializeField] float growAmount = .01f;

	[ContextMenu("Init")]
	public void Init()
	{
		transform.position = UtilClass.WorldOrigin;
		transform.position += Vector3.up * (UtilClass.CubeHeight / 2);
		Vector3 scale = UtilClass.CubeScale;
		scale.y = .001f;
		transform.localScale = scale;

		gameObject.SetActive(true);
		Invoke(nameof(Disable), duration);
	}

	private void Update()
	{
		Vector3 xz = new Vector3(1, 0, 1);
		transform.localScale += growAmount * Time.deltaTime * xz;
	}

	private void Disable()
	{
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.ReturnToPool(this);
	}
}
