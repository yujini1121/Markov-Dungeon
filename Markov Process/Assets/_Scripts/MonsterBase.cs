using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] private float searchRadius;

    public enum State
    {
        Roaming,
        Following,
        Charging,
        Attack,
        Cooldown
    }
    public State curState = State.Roaming;


    private void Update()
    {
        Debug.Log($"{name} : {curState}");
        HandleState();
    }

    private void HandleState()
    {
        switch (curState)
        {
            case State.Roaming:
                Roam();
                break;
            case State.Following:
                Follow();
                break;
            case State.Charging:
                Charge();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Cooldown:
                Cooldown();
                break;
        }
    }

    private void Roam() { /* �ι� ���� ���� */ }
    private void Follow() { /* ���� ���� ���� */ }
    private void Charge() { /* ��¡ ���� ���� */ }
    private void Attack() { /* ���� ���� ���� */ }
    private void Cooldown() { /* ��ٿ� ���� ���� */ }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Handles.color = GetStateColor();
        Handles.DrawWireDisc(transform.position, Vector3.forward, searchRadius);
    }

    private Color GetStateColor()
    {
        return curState switch
        {
            State.Roaming   => Color.green,
            State.Following => Color.blue,
            State.Charging  => Color.yellow,
            State.Attack    => Color.red,
            State.Cooldown  => Color.black,
            _               => Color.white      // default
        };
    }
}
