using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDeadZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<MyEnemy>().HP -= 9999;
        }
    }
}
