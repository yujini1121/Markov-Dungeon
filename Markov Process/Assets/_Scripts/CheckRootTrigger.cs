using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    이 스크립트는 플레이어가 루트 선택 트리거에 닿았을 때
    던전 매니저를 호출하여 다음 방을 생성하고,  
    플레이어의 이동을 일시적으로 막는 역할을 합니다.
*/

public class CheckRootTrigger : MonoBehaviour
{
    [SerializeField] private bool isHardRoute;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();
            if (dungeonManager != null)
            {
                dungeonManager.GenerateNextRoom(isHardRoute);

                other.GetComponent<PlayerController>().MoveControlSwitch(false);
            }
            else
            {
                Debug.LogError("DungeonManager를 찾을 수 없습니다!");
            }
        }
    }
}