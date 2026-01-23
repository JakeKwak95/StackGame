using TMPro;
using UnityEngine;

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
		Debug.Log("Game Over! Final Score: " + Score);
		Time.timeScale = 0f;
		InputManager.Instance.enabled = false;
	}
}
