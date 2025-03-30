using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRootTrigger : MonoBehaviour
{
    [SerializeField] private bool isHardRoute; // 이 트리거가 어려운 루트인지 여부

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name}이(가) {gameObject.name} 트리거에 감지됨"); // 로그 추가
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 감지됨! 다음 방 생성 시작");
            DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();
            if (dungeonManager != null)
            {
                dungeonManager.GenerateNextRoom(isHardRoute);
            }
            else
            {
                Debug.LogError("DungeonManager를 찾을 수 없습니다!");
            }
        }
    }
}