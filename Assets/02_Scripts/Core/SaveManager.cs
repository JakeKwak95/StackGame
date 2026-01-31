using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager Instance { get; private set; }


	void Awake()
	{
		// Singleton pattern
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void SaveScore(int score)
	{
		// PlayerPrefs는 Unity에서 제공하는 간단한 데이터 저장 시스템입니다. 키-값 쌍으로 데이터를 저장하며, 주로 설정값이나 간단한 게임 데이터를 저장하는 데 사용됩니다. 
		PlayerPrefs.SetInt("HighScore", score);
		PlayerPrefs.Save(); // WebGL에서는 명시적 Save 필요
	}

	public int LoadScore()
	{
		return PlayerPrefs.GetInt("HighScore", 0);
	}

	/*public int LoadScore()
	{
		// 하이스코어 불러오기
		return PlayerPrefs.GetInt("HighScore");
	}*/
}