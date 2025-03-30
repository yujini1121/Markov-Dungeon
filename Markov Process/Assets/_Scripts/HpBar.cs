using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HpBar : MonoBehaviour
{
    public float MaxHP = 100;
    public float currentHP;
    public Slider HPBar;
    public Transform player;

    void Start()
    {
        currentHP = MaxHP;
        HPBar.maxValue = MaxHP;
        HPBar.value = currentHP;
    }

    void Update()
    {
        Vector3 worldPosition = player.position + new Vector3(0, 2, 0); // 플레이어 위에 위치하도록 조정
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        HPBar.transform.position = screenPosition;

        HPBar.value = currentHP;
    }

    public void Attack()
    {
        currentHP -= 10;
    }
}
