using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRootTrigger : MonoBehaviour
{
	[SerializeField] private bool isHardRoute; // 이 트리거가 어려운 루트인지 여부

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();
			if (dungeonManager != null)
			{
				dungeonManager.GenerateNextRoom(isHardRoute); // 선택한 루트 반영
			}
		}
	}
}
