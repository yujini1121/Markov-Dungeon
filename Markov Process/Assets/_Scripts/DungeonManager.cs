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
    public Transform roomSpawnPoint;
    public List<RoomPrefab> roomPrefabs;
    private GameObject currentRoomInstance;
    private RoomType currentRoom = RoomType.EmptyRoom;

    private Dictionary<RoomType, Dictionary<RoomType, float>> easyRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
    {
        { RoomType.EmptyRoom, new Dictionary<RoomType, float>() { { RoomType.RestRoom, 0.6f }, { RoomType.TreasureRoom, 0.4f } } },
        { RoomType.RestRoom, new Dictionary<RoomType, float>() { { RoomType.EmptyRoom, 0.5f }, { RoomType.TreasureRoom, 0.3f }, { RoomType.RestRoom, 0.2f } } },
        { RoomType.TreasureRoom, new Dictionary<RoomType, float>() { { RoomType.RestRoom, 0.5f }, { RoomType.EmptyRoom, 0.3f }, { RoomType.TreasureRoom, 0.2f } } },
    };

    private Dictionary<RoomType, Dictionary<RoomType, float>> hardRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
    {
        { RoomType.EmptyRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.6f }, { RoomType.TrapRoom, 0.4f } } },
        { RoomType.MonsterRoom, new Dictionary<RoomType, float>() { { RoomType.TrapRoom, 0.5f }, { RoomType.TreasureRoom, 0.3f }, { RoomType.MonsterRoom, 0.2f } } },
        { RoomType.TrapRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.5f }, { RoomType.EmptyRoom, 0.3f }, { RoomType.TrapRoom, 0.2f } } },
    };

    public void GenerateNextRoom(bool isHardRoute)
    {
        RoomType nextRoomType = GetNextRoom(currentRoom, isHardRoute);

        if (currentRoomInstance != null)
        {
            Destroy(currentRoomInstance);
        }

        RoomPrefab roomPrefab = roomPrefabs.Find(r => r.roomType == nextRoomType);
        if (roomPrefab != null)
        {
            currentRoomInstance = Instantiate(roomPrefab.prefab, roomSpawnPoint.position, Quaternion.identity);
            Debug.Log($"새 방 생성됨: {nextRoomType}, 루트: {(isHardRoute ? "어려운" : "쉬운")}");
        }
        else
        {
            Debug.LogError("프리팹을 찾을 수 없음: " + nextRoomType);
        }
    }

    private RoomType GetNextRoom(RoomType currentRoom, bool isHardRoute)
    {
        Dictionary<RoomType, float> probabilities = isHardRoute ? hardRouteProbabilities[currentRoom] : easyRouteProbabilities[currentRoom];

        float randomValue = Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        foreach (var room in probabilities)
        {
            cumulativeProbability += room.Value;
            if (randomValue <= cumulativeProbability)
            {
                return room.Key;
            }
        }
        return currentRoom;
    }

    public void PlayerEnteredNewRoom(RoomType enteredRoomType)
    {
        currentRoom = enteredRoomType;
        Debug.Log($"플레이어가 방에 입장함: {enteredRoomType}. 다음 방 선택 대기.");
    }
}

[System.Serializable]
public class RoomPrefab
{
    public RoomType roomType;
    public GameObject prefab;
}
