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

		// 월드 오리진에 따른 보정
		if (movingAxisVector == Vector3.forward)
		{
			newPos.x += UtilClass.WorldOrigin.x;
		}
		else
		{
			newPos.z += UtilClass.WorldOrigin.z;
		}

		// 위치 적용
		transform.position = newPos;

		// 스케일 적용
		transform.localScale = UtilClass.CubeScale;
	}

	public void Stop()
	{
		bool isAxisZ = movingAxisVector == Vector3.forward;
		float missAmount = UtilClass.GetMissAmount(transform.position);

		// 절반을 넘었는지 체크
		int offsetSign = GetOffsetSign(isAxisZ);

		// 1. CubeScale 업데이트
		UpdateCubeScale(isAxisZ, missAmount);

		// 2. Scale 오프셋 계산
		Vector3 offsetByScale = CalculateScaleOffset(isAxisZ, missAmount, offsetSign);

		// 3. WorldOrigin 갱신
		UpdateWorldOrigin(offsetByScale);

		// 4. 블록 트랜스폼 업데이트
		UpdateTransform();

		MakeMissedPart(isAxisZ, missAmount, offsetSign);

		enabled = false;
	}

	// 절반을 넘었는지 체크
	private int GetOffsetSign(bool isAxisZ)
	{
		int offsetSign;
		if (isAxisZ)
		{
			offsetSign = transform.position.z > UtilClass.WorldOrigin.z ? -1 : 1;
		}
		else
		{
			offsetSign = transform.position.x > UtilClass.WorldOrigin.x ? -1 : 1;
		}

		return offsetSign;
	}

	private void MakeMissedPart(bool isAxisZ, float missAmount, int offsetSign)
	{
		// 잘려나갈 블록 부분 생성
		GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		newCube.AddComponent<Rigidbody>();

		Vector3 newPos = UtilClass.WorldOrigin;
		Vector3 newScale = UtilClass.CubeScale;
		if (isAxisZ)
		{
			newPos.z += (UtilClass.CubeScale.z + missAmount) / 2 * -offsetSign;
			newScale.z = missAmount;
		}
		else
		{
			newPos.x += (UtilClass.CubeScale.x + missAmount) / 2 * -offsetSign;
			newScale.x = missAmount;
		}

		newCube.transform.position = newPos;
		newCube.transform.localScale = newScale;
	}

	// 1. CubeScale 업데이트
	private void UpdateCubeScale(bool isAxisZ, float missAmount)
	{
		if (isAxisZ)
			UtilClass.CubeScale.z -= missAmount;
		else
			UtilClass.CubeScale.x -= missAmount;
	}

	// 2. Scale 오프셋 계산
	private Vector3 CalculateScaleOffset(bool isAxisZ, float missAmount, int offsetSign)
	{
		Vector3 offsetByScale = Vector3.zero;
		if (isAxisZ)
		{
			offsetByScale.z = missAmount / 2f * offsetSign;
		}
		else
		{
			offsetByScale.x = missAmount / 2f * offsetSign;
		}
		return offsetByScale;
	}

	// 3. WorldOrigin 갱신
	private void UpdateWorldOrigin(Vector3 offset)
	{
		UtilClass.WorldOrigin = transform.position + offset;
		UtilClass.WorldOrigin.y = transform.position.y;
	}

	// 4. 블록 트랜스폼 업데이트
	private void UpdateTransform()
	{
		transform.position = UtilClass.WorldOrigin;
		transform.localScale = UtilClass.CubeScale;
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
