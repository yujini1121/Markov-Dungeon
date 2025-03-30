using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    이 스크립트는 카메라 이동을 제어하는 역할을 합니다.  
    방 이동 시 카메라가 부드럽게 따라가도록 설정하며,  
    특정 위치(다음 방 위치)에 도달하면 플레이어의 이동을 다시 활성화합니다.
*/

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera virCamera;
    public Transform nextRoomTrans;
    public Transform cameraTarget;
    public float threshold = 0.01f;

    private void Start()
    {
        virCamera = GetComponent<CinemachineVirtualCamera>();
        //virCamera.Follow = nextRoomTrans;
    }

    public void CameraMove()
    {
        cameraTarget.position = nextRoomTrans.position;
        virCamera.Follow = cameraTarget;

        StartCoroutine(MoveDelay());
    }

    private IEnumerator MoveDelay()
    {
        while (true)
        {
            if (cameraTarget.position.y - transform.position.y < threshold)
            {
                break;
            }
            yield return null;
        }

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().MoveControlSwitch(true, cameraTarget.position.y);
    }
}