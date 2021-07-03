using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyTrap : MonoBehaviour
{
    [SerializeField]
    private MyEnemyAttack trap;
    [SerializeField]
    private Transform shootingPos;
    [SerializeField]
    private Vector2 direction;


    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            Instantiate(trap, shootingPos.position, Quaternion.identity).Execute(direction);
        }).
        AppendInterval(1.5f).
        SetLoops(-1);
    }



}
