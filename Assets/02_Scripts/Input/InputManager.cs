using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	// 클릭 입력 액션 레퍼런스
	[SerializeField] InputActionReference clickInput;

	// 클릭 이벤트 델리게이트
	public static Action OnClick;

	// 구독 및 구독 해제
	private void OnEnable()
	{
		clickInput.action.performed += HandleClick;
	}

	private void OnDisable()
	{
		clickInput.action.performed -= HandleClick;
	}

	// 클릭 이벤트 처리
	private void HandleClick(InputAction.CallbackContext obj)
	{
		OnClick?.Invoke();
	}

	// 애플리케이션 종료 시 이벤트 초기화
	private void OnApplicationQuit()
	{
		OnClick = null;
	}
}
