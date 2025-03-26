using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRootTrigger : MonoBehaviour
{
	[SerializeField] private bool isHardRoute; // �� Ʈ���Ű� ����� ��Ʈ���� ����

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();
			if (dungeonManager != null)
			{
				dungeonManager.GenerateNextRoom(isHardRoute); // ������ ��Ʈ �ݿ�
			}
		}
	}
}
