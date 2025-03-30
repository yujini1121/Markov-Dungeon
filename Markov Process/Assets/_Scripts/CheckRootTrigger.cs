using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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