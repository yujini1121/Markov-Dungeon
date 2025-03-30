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
    public Transform nextRoomSpawnPoint;  // 방 생성 위치를 지정할 빈 게임 오브젝트
    public List<RoomPrefab> roomPrefabs;
    private GameObject currentRoomInstance;
    private RoomType currentRoom = RoomType.EmptyRoom;
    private bool? currentRoute = null;  // 루트 선택을 나타내는 변수. true = 쉬운 루트, false = 어려운 루트

    private Dictionary<RoomType, Dictionary<RoomType, float>> easyRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
    {
        { RoomType.EmptyRoom, new Dictionary<RoomType, float>() { { RoomType.RestRoom, 0.5f }, { RoomType.TrapRoom, 0.5f } } },
        { RoomType.RestRoom, new Dictionary<RoomType, float>() { { RoomType.EmptyRoom, 0.4f }, { RoomType.TrapRoom, 0.4f }, { RoomType.RestRoom, 0.2f } } },
        { RoomType.TreasureRoom, new Dictionary<RoomType, float>() { { RoomType.RestRoom, 0.2f }, { RoomType.EmptyRoom, 0.3f }, { RoomType.TrapRoom, 0.5f } } },
        { RoomType.MonsterRoom, new Dictionary<RoomType, float>() { { RoomType.TrapRoom, 0.5f }, { RoomType.EmptyRoom, 0.3f }, { RoomType.MonsterRoom, 0.2f } } },
        { RoomType.TrapRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.4f }, { RoomType.EmptyRoom, 0.5f }, { RoomType.RestRoom, 0.1f } } },
    };

    private Dictionary<RoomType, Dictionary<RoomType, float>> hardRouteProbabilities = new Dictionary<RoomType, Dictionary<RoomType, float>>()
    {
        { RoomType.EmptyRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.6f }, { RoomType.TreasureRoom, 0.4f } } },
        { RoomType.MonsterRoom, new Dictionary<RoomType, float>() { { RoomType.TrapRoom, 0.4f }, { RoomType.TreasureRoom, 0.3f }, { RoomType.MonsterRoom, 0.3f } } },
        { RoomType.TrapRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.3f }, { RoomType.EmptyRoom, 0.6f }, { RoomType.TreasureRoom, 0.1f } } },
        { RoomType.RestRoom, new Dictionary<RoomType, float>() { { RoomType.EmptyRoom, 0.5f }, { RoomType.TreasureRoom, 0.2f }, { RoomType.MonsterRoom, 0.3f } } },
        { RoomType.TreasureRoom, new Dictionary<RoomType, float>() { { RoomType.MonsterRoom, 0.5f }, { RoomType.EmptyRoom, 0.2f }, { RoomType.TreasureRoom, 0.3f } } },
    };


    public void GenerateNextRoom(bool isHardRoute)
    {
        // 현재 루트가 초기화되어 있지 않으면 초기화
        if (currentRoute == null)
        {
            currentRoute = isHardRoute;
        }

        RoomType nextRoomType = GetNextRoom(currentRoom, isHardRoute);

        RoomPrefab roomPrefab = roomPrefabs.Find(r => r.roomType == nextRoomType);
        if (roomPrefab != null)
        {
            // 기존 방을 파괴하지 않고 새로운 방을 생성
            Vector3 newRoomPosition = nextRoomSpawnPoint.position;  // 현재 위치에서 방 생성
            currentRoomInstance = Instantiate(roomPrefab.prefab, newRoomPosition, Quaternion.identity);
            Debug.Log($"새 방 생성됨: {nextRoomType}, 루트: {(isHardRoute ? "어려운" : "쉬운")}");

            GameObject.Find("Virtual Camera").GetComponent<CameraController>().CameraMove();

            // 방이 생성되면 Spawn Point 위치를 y좌표로 +15만큼 이동
            nextRoomSpawnPoint.position = new Vector3(nextRoomSpawnPoint.position.x, nextRoomSpawnPoint.position.y + 15f, nextRoomSpawnPoint.position.z);

            // 현재 방을 갱신
            currentRoom = nextRoomType;
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

        // 방에 들어갈 때마다 루트를 초기화하여 다음 방 선택 시 올바르게 루트를 선택하도록 만듦
        currentRoute = null;  // 루트 선택 초기화
        Debug.Log($"플레이어가 방에 입장함: {enteredRoomType}. 다음 방 선택 대기.");
    }
}

[System.Serializable]
public class RoomPrefab
{
    public RoomType roomType;
    public GameObject prefab;
}
