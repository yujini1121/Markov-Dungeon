using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    �� ��ũ��Ʈ�� �÷��̾ Ư�� �濡 �������� ��,
    ���� �Ŵ������� �ش� ���� ������ �����Ͽ�  
    ���� �� ������ �����ϴ� ������ �մϴ�.
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
                Debug.LogError("DungeonManager�� ã�� �� �����ϴ�!");
            }
        }
    }
}
