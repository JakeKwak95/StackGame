using UnityEngine;

public class Tester : MonoBehaviour
{
	[SerializeField, EnumButtons] SFXType sfxType;

	[ContextMenu("Test")]
	void Test()
	{
		AudioManager.Instance.PlaySFX(sfxType);
	}
}
