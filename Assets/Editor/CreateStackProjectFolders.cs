using UnityEditor;
using UnityEngine;
using System.IO;

public static class CreateStackProjectFolders
{
	[MenuItem("Tools/Stack/Create Project Folder Structure")]
	public static void CreateFolders()
	{
		string root = "Assets";

		// 최상위 폴더
		CreateFolder(root, "01_Scenes");
		CreateFolder(root, "02_Scripts");
		CreateFolder(root, "03_Prefabs");
		CreateFolder(root, "04_Materials");
		CreateFolder(root, "05_Audio");

		// Scripts 하위
		CreateFolder($"{root}/02_Scripts", "Core");
		CreateFolder($"{root}/02_Scripts", "Block");
		CreateFolder($"{root}/02_Scripts", "Input");
		CreateFolder($"{root}/02_Scripts", "Camera");
		CreateFolder($"{root}/02_Scripts", "UI");
		CreateFolder($"{root}/02_Scripts", "System");
		CreateFolder($"{root}/02_Scripts", "Util");

		// Prefabs 하위
		CreateFolder($"{root}/03_Prefabs", "Block");
		CreateFolder($"{root}/03_Prefabs", "UI");

		// Materials 하위
		CreateFolder($"{root}/04_Materials", "Block");
		CreateFolder($"{root}/04_Materials", "Background");

		// Audio 하위
		CreateFolder($"{root}/05_Audio", "SFX");
		CreateFolder($"{root}/05_Audio", "BGM");

		AssetDatabase.Refresh();
		Debug.Log("✅ Stack 프로젝트 폴더 구조 생성 완료");
	}

	private static void CreateFolder(string parent, string folderName)
	{
		string path = Path.Combine(parent, folderName);
		if (!AssetDatabase.IsValidFolder(path))
		{
			AssetDatabase.CreateFolder(parent, folderName);
		}
	}
}
