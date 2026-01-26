using System;
using TMPro;
using UnityEngine;

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
	}
}
