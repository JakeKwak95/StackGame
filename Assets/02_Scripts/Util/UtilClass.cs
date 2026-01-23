using UnityEngine;

public static class UtilClass
{
	public static readonly float CubeHeight = 0.2f;
	public static Vector3 CubeScale = new Vector3(1f, CubeHeight, 1f);

	public static Vector3 WorldOrigin = Vector3.zero;

	// 포지션을 받아서 월드 오리진과의 XZ 평면에서의 거리를 계산
	public static float GetMissAmount(Vector3 pos)
	{
		// 블록이 쌓이는 높이는 고려하지 않기 때문에, Y값은 무시하고 XZ 평면에서의 거리 계산
		Vector2 posXZ = new Vector2(pos.x, pos.z);
		Vector2 originXZ = new Vector2(WorldOrigin.x, WorldOrigin.z);
		return Vector2.Distance(posXZ, originXZ);
	}
}
