using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerAttack_Out : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (col2D.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            Debug.Log("Enemy Damage: " + damage);
        }
    }
}
