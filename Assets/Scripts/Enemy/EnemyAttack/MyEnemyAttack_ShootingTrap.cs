using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyAttack_ShootingTrap : MyEnemyAttack
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] int damage;
    [SerializeField] float speed;
    public override void Execute(Vector3 _target)
    {
        transform.Rotate(new Vector3(0, 0, Vector3.Angle(_target, Vector3.right) * (_target.y > 0 ? 1 : -1)));
        rb2D.velocity = _target.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        if (col2D.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Debug.Log("Player Damage : " + damage);
            Destroy(gameObject);
        }
        else if (col2D.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain")))
        {
            Destroy(gameObject);
        }
    }
}
