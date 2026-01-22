using UnityEngine;

public class BlockMovement : MonoBehaviour
{
	// 이동 속도
	[SerializeField] float speed = 2.5f;
	// 이동 거리
	[SerializeField] float distance = 1.25f;

	// 이동 축 벡터, 디버깅 용으로 노출
	[SerializeField] Vector3 movingAxisVector = Vector3.forward;

	float direction = -1f;
	float timer = 0f;

	private void Awake()
	{
		transform.position = movingAxisVector * distance;
	}

	private void FixedUpdate()
	{
		// direction 방향으로 이동
		transform.Translate(direction * speed * Time.fixedDeltaTime * movingAxisVector);

		timer += Time.fixedDeltaTime;
		// 끝에서 끝까지 왕복하는 시간 계산
		if (timer > distance * 2 / speed)
		{
			// 방향 전환
			direction *= -1f;
			timer = 0f;
		}
	}
}
