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
    public Transform roomSpawnPoint; // ���� ������ ��ġ
    public List<RoomPrefab> roomPrefabs; // �� ������ ����Ʈ (�ν����Ϳ��� ����)
    private GameObject currentRoomInstance; // ���� ������ ��

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

    public RoomType GetNextRoom(RoomType currentRoom)
    {
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

    public void GenerateNextRoom(RoomType currentRoom)
    {
        RoomType nextRoomType = GetNextRoom(currentRoom);

        // ���� �� ����
        if (currentRoomInstance != null)
        {
            Destroy(currentRoomInstance);
        }

        // �� ������ ã��
        RoomPrefab roomPrefab = roomPrefabs.Find(r => r.roomType == nextRoomType);
        if (roomPrefab != null)
        {
            currentRoomInstance = Instantiate(roomPrefab.prefab, roomSpawnPoint.position, Quaternion.identity);
            Debug.Log("������ ��: " + nextRoomType);
        }
        else
        {
            Debug.LogError("�������� ã�� �� ����: " + nextRoomType);
        }
    }

    void Start()
    {
        GenerateNextRoom(RoomType.EmptyRoom);
    }
}

[System.Serializable]
public class RoomPrefab
{
    public RoomType roomType;
    public GameObject prefab;
}
