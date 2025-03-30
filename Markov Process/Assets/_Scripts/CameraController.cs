using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    �� ��ũ��Ʈ�� ī�޶� �̵��� �����ϴ� ������ �մϴ�.  
    �� �̵� �� ī�޶� �ε巴�� ���󰡵��� �����ϸ�,  
    Ư�� ��ġ(���� �� ��ġ)�� �����ϸ� �÷��̾��� �̵��� �ٽ� Ȱ��ȭ�մϴ�.
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