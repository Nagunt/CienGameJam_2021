using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack_BWolf : MyEnemyAttack
{
    [SerializeField] SpriteRenderer model;
    [SerializeField] int damage;
    public override void Execute(Vector3 _target)
    {
        gameObject.SetActive(true);
        StartCoroutine(_Routine());

        IEnumerator _Routine()
        {
            yield return new WaitForSeconds(attackDelay);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (col2D.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            col2D.GetComponentInParent<MyPlayer>().HP -= 1;
        }
    }
}
