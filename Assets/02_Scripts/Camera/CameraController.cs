using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// 카메라 타겟 트랜스폼
	[SerializeField] Transform camTarget;
	// 카메라 타겟 위치
	Vector3 targetPos;
	
	// 타겟 위치까지 걸리는 시간
	[SerializeField] float timeToMove = 0.25f;

	// 이동 중인지 여부
	bool isBusy = false;

	// 게임 오버 시 줌 아웃 시간
	[SerializeField] float zoomOutTime = 0.5f;
	[SerializeField] CinemachineTargetGroup cinemachineTargetGroup;

	// 무시할 업 카운트
	[SerializeField] int ignoreAmount = 3;
	int upCount = 0;

	private void OnEnable()
	{
		// 클릭 이벤트 구독
		InputManager.OnClick += UpdateTargetPos;
		GameManager.OnGameOver += OnGameOver;
	}
	private void OnDisable()
	{
		InputManager.OnClick -= UpdateTargetPos;
		GameManager.OnGameOver -= OnGameOver;
	}


	private void UpdateTargetPos()
	{
		// 무시할 업 카운트 처리
		if (upCount < ignoreAmount)
		{
			upCount++;
			return;
		}

		// 이미 이동 중이면 즉시 위치 설정
		if (isBusy)
		{
			StopAllCoroutines();
			camTarget.position = targetPos;
			isBusy = false;
		}

		// 코루틴 시작
		StartCoroutine(CoUpdateTargetPos());
	}

	IEnumerator CoUpdateTargetPos()
	{
		// 바쁨 상태 설정
		isBusy = true;

		// 흘러간 시간
		float elapsedTime = 0f;
		// 시작 위치
		Vector3 startingPos = camTarget.position;
		// 목표 위치
	    targetPos = startingPos + Vector3.up * UtilClass.CubeHeight;

		// 목표 위치까지 이동
		while (elapsedTime < timeToMove)
		{
			// 선형 보간으로 위치 업데이트
			camTarget.position = Vector3.Lerp(startingPos, targetPos, (elapsedTime / timeToMove));
			// 경과 시간 증가
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		// 최종 위치 설정
		camTarget.position = targetPos;
		// 바쁨 상태 해제
		isBusy = false;
	}

	private void OnGameOver()
	{
		StartCoroutine(CoOnGameOver());
	}

	IEnumerator CoOnGameOver()
	{
		// 흘러간 시간
		float elapsedTime = 0f;

		while (elapsedTime < zoomOutTime)
		{
			// 카메라 타겟 그룹의 첫 번째 타겟 가중치 업데이트
			cinemachineTargetGroup.Targets[0].Weight = Mathf.Lerp(0f, 1f, elapsedTime / zoomOutTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		cinemachineTargetGroup.Targets[0].Weight = 1f;
	}
}
