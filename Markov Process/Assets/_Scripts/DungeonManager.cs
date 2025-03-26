using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
	EmptyRoom,
	TreasureRoom,
	TrapRoom,
	RestRoom,
	MonsterRoom
}

public class DungeonManager : MonoBehaviour
{
	public Transform easyRoomSpawnPoint; // ���� ��Ʈ �� ���� ��ġ
	public Transform hardRoomSpawnPoint; // ����� ��Ʈ �� ���� ��ġ
	public List<RoomPrefab> roomPrefabs; // �� ������ ����Ʈ (�ν����Ϳ��� ����)

	private GameObject currentRoomInstance; // ���� ������ ��
	private bool isHardRoute = false; // �÷��̾ ������ ��Ʈ (false: ���� ��Ʈ, true: ����� ��Ʈ)

	private Dictionary<RoomType, Dictionary<RoomType, float>> easyRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
	{
		{
			RoomType.EmptyRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.RestRoom, 0.6f },
				{ RoomType.TreasureRoom, 0.4f }
			}
		},
		{
			RoomType.RestRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.5f },
				{ RoomType.TreasureRoom, 0.3f },
				{ RoomType.MonsterRoom, 0.2f }	
			}
		}
	};

	private Dictionary<RoomType, Dictionary<RoomType, float>> hardRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
	{
		{
			RoomType.EmptyRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.MonsterRoom, 0.5f },
				{ RoomType.TrapRoom, 0.5f }
			}
		},
		{
			RoomType.MonsterRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.TrapRoom, 0.3f },
				{ RoomType.TreasureRoom, 0.2f },
				{ RoomType.MonsterRoom, 0.5f }
			}
		}
	};

	public RoomType GetNextRoom(RoomType currentRoom, bool isHardRoute)
	{
		Dictionary<RoomType, Dictionary<RoomType, float>> transitionProbabilities =
			isHardRoute ? hardRouteProbabilities : easyRouteProbabilities;

		if (!transitionProbabilities.ContainsKey(currentRoom))
			return currentRoom;

		float randomValue = Random.Range(0f, 1f);
		float cumulativeProbability = 0f;

		foreach (var room in transitionProbabilities[currentRoom])
		{
			cumulativeProbability += room.Value;
			if (randomValue <= cumulativeProbability)
			{
				return room.Key;
			}
		}
		return currentRoom;
	}

	public void GenerateNextRoom(RoomType currentRoom, bool isHardRoute)
	{
		RoomType nextRoomType = GetNextRoom(currentRoom, isHardRoute);
		Transform spawnPoint = isHardRoute ? hardRoomSpawnPoint : easyRoomSpawnPoint;

		if (currentRoomInstance != null)
		{
			Destroy(currentRoomInstance);
		}

		RoomPrefab roomPrefab = roomPrefabs.Find(r => r.roomType == nextRoomType);
		if (roomPrefab != null)
		{
			currentRoomInstance = Instantiate(roomPrefab.prefab, spawnPoint.position, Quaternion.identity);
			Debug.Log($"������ ��: {nextRoomType}, ��Ʈ: {(isHardRoute ? "�����" : "����")}");
		}
		else
		{
			Debug.LogError("�������� ã�� �� ����: " + nextRoomType);
		}
	}

	public void PlayerChoseEasyRoute()
	{
		isHardRoute = false;
		GenerateNextRoom(RoomType.EmptyRoom, isHardRoute);
	}

	public void PlayerChoseHardRoute()
	{
		isHardRoute = true;
		GenerateNextRoom(RoomType.EmptyRoom, isHardRoute);
	}

	void Start()
	{
		GenerateNextRoom(RoomType.EmptyRoom, isHardRoute);
	}
}

[System.Serializable]
public class RoomPrefab
{
	public RoomType roomType;
	public GameObject prefab;
}
