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

    private Sequence shootingSequence;


    private void Start()
    {
        shootingSequence = DOTween.Sequence();
        shootingSequence.AppendCallback(() =>
        {
            Instantiate(trap, shootingPos.position, Quaternion.identity).Execute(direction);
        }).
        AppendInterval(1.5f).
        SetLoops(-1);
    }

    private void OnDestroy()
    {
        shootingSequence.Kill();
    }



}
