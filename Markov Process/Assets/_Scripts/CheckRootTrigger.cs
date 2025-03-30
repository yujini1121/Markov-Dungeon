using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRootTrigger : MonoBehaviour
{
	[SerializeField] private bool isHardRoute; // �� Ʈ���Ű� ����� ��Ʈ���� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name}��(��) {gameObject.name} Ʈ���ſ� ������"); // �α� �߰�
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ������! ���� �� ���� ����");
            DungeonManager dungeonManager = FindObjectOfType<DungeonManager>();
            if (dungeonManager != null)
            {
                dungeonManager.GenerateNextRoom(isHardRoute);
            }
            else
            {
                Debug.LogError("DungeonManager�� ã�� �� �����ϴ�!");
            }
        }
    }
}
