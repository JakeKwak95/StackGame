using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	Playing,
	GameOver
}

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] TextMeshProUGUI scoreText;
	public int Score { get; private set; } = 0;
	public void AddScore()
	{
		Score++;
		scoreText.SetText(Score.ToString());
	}

	public GameState CurrentState { get; private set; } = GameState.Playing;
	public static Action OnGameOver;

	// 재시작 패널 참조
	[SerializeField] Button restartPanel;

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
	}

	public void GameOver()
	{
		OnGameOver?.Invoke();

		Debug.Log("Game Over! Final Score: " + Score);
		CurrentState = GameState.GameOver;
		InputManager.Instance.enabled = false;
		AudioManager.Instance.PlaySFX(SFXType.GameOver);

		// 재시작 패널 활성화
		restartPanel.gameObject.SetActive(true);
	}

	// 게임 재시작 메서드
	public void RestartGame()
	{
		// 글로벌 정적 변수 초기화
		UtilClass.WorldOrigin = Vector3.zero;
		UtilClass.CubeScale = new Vector3(1f, UtilClass.CubeHeight, 1f);

		UnityEngine.SceneManagement.SceneManager.LoadScene(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
