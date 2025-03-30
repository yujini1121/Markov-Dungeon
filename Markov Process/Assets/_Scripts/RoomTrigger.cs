using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    이 스크립트는 플레이어가 특정 방에 진입했을 때,
    던전 매니저에게 해당 방의 유형을 전달하여  
    현재 방 정보를 갱신하는 역할을 합니다.
*/

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomType roomType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();

            if (dungeonManager != null)
            {
                dungeonManager.PlayerEnteredNewRoom(roomType);
            }
            else
            {
                Debug.LogError("DungeonManager를 찾을 수 없습니다!");
            }
        }
    }
}
