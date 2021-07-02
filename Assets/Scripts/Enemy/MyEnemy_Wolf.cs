using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyEnemy_Wolf : MyEnemy
{
    [Header("- AI")]
    [SerializeField] private string phase;
    [SerializeField] private Transform target;
    [Header("- Area")]
    [SerializeField] private Collider2D detectArea;
    [SerializeField] private Collider2D attackDetectArea;
    [SerializeField] private ContactFilter2D contactFilter2D;
    [Header("- Attack")]
    [SerializeField] private Transform attack;
    [SerializeField] private MyEnemyAttack attack_1;

    private List<Collider2D> detect;

    protected override void Init()
    {
        HP = 5;
        phase = "Wait";
        detect = new List<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col2D)
    {
        if (col2D.collider.CompareTag("Player"))
        {
            // 플레이어 몸통 히트 대미지

        }
    }
    float delay = 0f;
    float attackDelay = 1.5f;
    private void Update()
    {
        detect.Clear();
        switch (phase)
        {
            case "Wait":
                {
                    rb2D.velocity = Vector2.zero;
                    if (Physics2D.OverlapCollider(detectArea, contactFilter2D, detect) > 0)
                    {
                        target = detect[0].transform;
                        phase = "Move";
                    }
                    break;
                }
            case "Move":
                {
                    if (Physics2D.OverlapCollider(detectArea, contactFilter2D, detect) <= 0)
                    {
                        target = null;
                        phase = "Wait";
                    }
                    if(Physics2D.OverlapCollider(attackDetectArea, contactFilter2D, detect) > 0)
                    {
                        if (IsGround)
                        {
                            phase = "Attack";
                        }
                    }
                    if (phase.Equals("Move"))
                    {
                        Move();
                    }
                    break;
                }
            case "Attack":
                {
                    rb2D.velocity = Vector2.zero;
                    model.flipX = target.position.x > transform.position.x;
                    attack.localRotation = Quaternion.Euler(0, target.position.x > transform.position.x ? 180 : 0, 0);
                    if (delay < attackDelay)
                    {
                        delay += Time.deltaTime;
                    }
                    else
                    {
                        delay = 0f;
                        if (Physics2D.OverlapCollider(attackDetectArea, contactFilter2D, detect) <= 0)
                        {
                            phase = "Move";
                        }
                        attack_1.Execute(target.position);
                    }
                    break;
                }
        }
    }


    private void Move()
    {
        if (target != null)
        {
            model.flipX = target.position.x > transform.position.x;
            attack.localRotation = Quaternion.Euler(0, target.position.x > transform.position.x ? 180 : 0, 0);
            if (IsGround)
            {
                if (target.position.y - transform.position.y > 0.5f)
                {
                    // 플레이어가 몹보다 위에 있을때

                    rb2D.velocity = new Vector2(rb2D.velocity.x, 1 * 20.0f);

                }
                else if (target.position.y - transform.position.y < -0.5f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                }
            }
            rb2D.velocity = new Vector2((target.position.x > transform.position.x ? 1 : -1) * 5.0f, rb2D.velocity.y);
        }
    }
}
