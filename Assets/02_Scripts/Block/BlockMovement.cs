using UnityEngine;

public class BlockMovement : MonoBehaviour
{
	// 이동 속도
	[SerializeField] float speed = 2.5f;
	// 이동 거리
	[SerializeField] float distance = 1.25f;
	Vector3 movingAxisVector = Vector3.forward;
	float direction = -1f;
	float timer = 0f;

	public void Init(int index, Vector3 axis)
	{
		// 이동 축 설정
		movingAxisVector = axis;

		// 초기 위치 설정
		Vector3 newPos = movingAxisVector * distance;
		// 인덱스와 유틸클래스의 높이값에 따라 높이 설정 
		newPos += index * UtilClass.CubeHeight * Vector3.up;
		// 위치 적용
		transform.position = newPos;
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
