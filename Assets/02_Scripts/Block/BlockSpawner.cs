using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	// 블록 프리팹
	[SerializeField] BlockMovement blockPrefab;
	// 현재 생성된 블록
	BlockMovement currentBlock;

	// 현재 블록 인덱스
	int blockIndex = 0;

	// 현재 이동 축
	Vector3 movingAxis = Vector3.forward;

	// 최초 실행 시 블록 생성
	private void Awake()
	{
		SpawnBlock();
	}

	// 클릭 이벤트 구독 및 해제
	private void OnEnable()
	{
		InputManager.OnClick += SpawnBlock;
	}
	private void OnDisable()
	{
		InputManager.OnClick -= SpawnBlock;
	}

	// 블록 생성 메서드
	public void SpawnBlock()
	{
		// 기존 블록 비활성화
		if (currentBlock)
			currentBlock.Stop();

		// 새 블록 생성 및 초기화
		currentBlock = Instantiate(blockPrefab, transform);
		currentBlock.Init(blockIndex, movingAxis);
		blockIndex++;

		SwitchMovingAxis();
	}

	// 이동 축 전환 메서드
	private void SwitchMovingAxis()
	{
		// 이동 축이 Z축이면 X축으로, X축이면 Z축으로 전환
		if (movingAxis == Vector3.forward)
			movingAxis = Vector3.right;
		else
			movingAxis = Vector3.forward;
	}
}