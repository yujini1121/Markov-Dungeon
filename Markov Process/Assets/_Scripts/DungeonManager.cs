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
	// 각 방에서 다음 방으로의 확률을 관리하는 딕셔너리
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

	// 현재 방에서 다음 방을 선택하는 함수
	public RoomType GetNextRoom(RoomType currentRoom)
	{
		float randomValue = Random.Range(0f, 1f);
		float cumulativeProbability = 0f;

		// 확률에 맞춰 다음 방을 선택
		foreach (var room in transitionProbabilities[currentRoom])
		{
			cumulativeProbability += room.Value;
			if (randomValue <= cumulativeProbability)
			{
				return room.Key;
			}
		}

		return currentRoom;  // 이건 드물게 발생 (혹시라도 확률 합이 1이 아닐 경우)
	}


	// 게임 시작시 예시
	void Start()
	{
		RoomType currentRoom = RoomType.EmptyRoom;  // 예시: 빈 방에서 시작
		RoomType nextRoom = GetNextRoom(currentRoom);
		Debug.Log("다음 방: " + nextRoom);
	}
}
