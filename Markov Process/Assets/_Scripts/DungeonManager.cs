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
	// �� �濡�� ���� �������� Ȯ���� �����ϴ� ��ųʸ�
	private Dictionary<RoomType, Dictionary<RoomType, float>> transitionProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
	{
		{
			RoomType.EmptyRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.5f },
				{ RoomType.TrapRoom, 0.5f }
			}
		},
		{
			RoomType.TrapRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.3f },
				{ RoomType.TreasureRoom, 0.2f },
				{ RoomType.RestRoom, 0.1f },
				{ RoomType.MonsterRoom, 0.3f },
				{ RoomType.TrapRoom, 0.1f }
			}
		},
		{
			RoomType.TreasureRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.3f },
				{ RoomType.TrapRoom, 0.2f },
				{ RoomType.RestRoom, 0.2f },
				{ RoomType.MonsterRoom, 0.2f },
				{ RoomType.TreasureRoom, 0.1f }
			}
		},
		{
			RoomType.RestRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.4f },
				{ RoomType.TrapRoom, 0.3f },
				{ RoomType.TreasureRoom, 0.1f },
				{ RoomType.MonsterRoom, 0.1f },
				{ RoomType.RestRoom, 0.1f }
			}
		},
		{
			RoomType.MonsterRoom, new Dictionary<RoomType, float>()
			{
				{ RoomType.EmptyRoom, 0.2f },
				{ RoomType.TrapRoom, 0.2f },
				{ RoomType.TreasureRoom, 0.1f },
				{ RoomType.RestRoom, 0.1f },
				{ RoomType.MonsterRoom, 0.4f }
			}
		}
	};

	// ���� �濡�� ���� ���� �����ϴ� �Լ�
	public RoomType GetNextRoom(RoomType currentRoom)
	{
		float randomValue = Random.Range(0f, 1f);
		float cumulativeProbability = 0f;

		// Ȯ���� ���� ���� ���� ����
		foreach (var room in transitionProbabilities[currentRoom])
		{
			cumulativeProbability += room.Value;
			if (randomValue <= cumulativeProbability)
			{
				return room.Key;
			}
		}

		return currentRoom;  // �̰� �幰�� �߻� (Ȥ�ö� Ȯ�� ���� 1�� �ƴ� ���)
	}


	// ���� ���۽� ����
	void Start()
	{
		RoomType currentRoom = RoomType.EmptyRoom;  // ����: �� �濡�� ����
		RoomType nextRoom = GetNextRoom(currentRoom);
		Debug.Log("���� ��: " + nextRoom);
	}
}
