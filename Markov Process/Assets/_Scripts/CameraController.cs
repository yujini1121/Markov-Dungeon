using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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