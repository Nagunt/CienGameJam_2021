using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy_SWolf : MyEnemy
{
    [SerializeField] protected Collider2D runawayArea;
    [Header("- Attack")]
    [SerializeField] protected Transform attack;
    [SerializeField] protected MyEnemyAttack attack_1;

    protected override void Init()
    {
        HP = 2;
        phase = MyEnemyPhase.IDLE;
        detect = new List<Collider2D>();
        base.Init();
    }

    private void OnCollisionEnter2D(Collision2D col2D)
    {
        if (isAlive)
        {
            if (col2D.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                // �÷��̾� ���� ��Ʈ �����
                Debug.Log("�����ġ��!");
            }
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            detect.Clear();
            switch (phase)
            {
                case MyEnemyPhase.IDLE:
                    {
                        if (lastPhase != phase)
                        {
                            // ������ ������ �Ͼ �����ӿ��� ����
                            Stop();

                            if (patrolArea.Length > 0) // ��Ʈ���� ���� ��
                            {
                                patrolIndex = 0;
                                patrolPos = new Vector2[patrolArea.Length];
                                for (int i = 0; i < patrolPos.Length; ++i)
                                {
                                    patrolPos[i] = patrolArea[i].position;
                                }
                            }
                            lastPhase = phase;
                        }
                        if (Physics2D.OverlapCollider(detectArea, contactFilter2D, detect) > 0)
                        {
                            target = detect[0].transform;
                            phase = MyEnemyPhase.MOVE;
                        }

                        if (phase == MyEnemyPhase.IDLE)
                        {
                            if (patrolArea.Length > 0)
                            {
                                if (transform.position.x > patrolPos[patrolIndex].x + minimumDistance || transform.position.x < patrolPos[patrolIndex].x - minimumDistance)
                                {
                                    Move(patrolPos[patrolIndex] - (Vector2)transform.position);
                                }
                                else
                                {
                                    if (++patrolIndex >= patrolPos.Length)
                                    {
                                        patrolIndex = 0;
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                        break;
                    }
                case MyEnemyPhase.MOVE:
                    {
                        if (lastPhase != phase)
                        {
                            // ������ ������ �Ͼ �����ӿ��� ����
                            Stop();
                            lastPhase = phase;
                        }

                        // ������ ���� ���� ����
                        if (Physics2D.OverlapCollider(detectArea, contactFilter2D, detect) <= 0)
                        {
                            target = null;
                            phase = MyEnemyPhase.IDLE;
                        }
                        if (Physics2D.OverlapCollider(attackDetectArea, contactFilter2D, detect) > 0)
                        {
                            if (IsGround)
                            {
                                phase = MyEnemyPhase.ATTACK;
                            }
                        }
                        // ������ ���� ���� ���� ��

                        // ������ ���� ��
                        if (phase == MyEnemyPhase.MOVE)
                        {
                            if (target != null)
                            {
                                Move(target.position - transform.position);
                            }
                        }
                        else
                        {
                            // ������ ���� ��

                        }
                        break;
                    }
                case MyEnemyPhase.ATTACK:
                    {
                        if (lastPhase != phase)
                        {
                            // ������ ������ �Ͼ �����ӿ��� ����
                            delay = 0f;
                            Stop();
                            attack.localRotation = Quaternion.Euler(0, target.position.x > transform.position.x ? 180 : 0, 0);
                            attackData = attack_1;
                            attackPhase = MyEnemyAttackPhase.BEFORE;
                            lastPhase = phase;
                        }

                        switch (attackPhase)
                        {
                            case MyEnemyAttackPhase.BEFORE:
                                {
                                    if (delay < attackData.attackBeforeDelay)
                                    {
                                        delay += Time.deltaTime;
                                    }
                                    else
                                    {
                                        delay = 0;
                                        attackData.Execute(target.position);
                                        attackPhase = MyEnemyAttackPhase.ATTACK;
                                    }
                                    break;
                                }
                            case MyEnemyAttackPhase.ATTACK:
                                {
                                    if (delay < attackData.attackDelay)
                                    {
                                        delay += Time.deltaTime;
                                    }
                                    else
                                    {
                                        attackPhase = MyEnemyAttackPhase.AFTER;
                                        delay = 0;
                                    }
                                    break;
                                }
                            case MyEnemyAttackPhase.AFTER:
                                {
                                    if (delay < attackData.attackAfterDelay)
                                    {
                                        delay += Time.deltaTime;
                                    }
                                    else
                                    {
                                        phase = MyEnemyPhase.SWAY;
                                        delay = 0;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case MyEnemyPhase.SWAY:
                    {
                        if (lastPhase != phase)
                        {
                            // ������ ������ �Ͼ �����ӿ��� ����
                            delay = 0;
                            lastPhase = phase;
                        }

                        if(delay < 2.0f)
                        {
                            if (Physics2D.OverlapCollider(runawayArea, contactFilter2D, detect) > 0)
                            {
                                if (target != null)
                                {
                                    Move(transform.position - target.position);
                                }
                            }
                            else
                            {
                                Stop();
                                // �ƹ��͵� ���ؿ�
                            }
                            delay += Time.deltaTime;
                        }
                        else
                        {
                            phase = MyEnemyPhase.IDLE;
                            delay = 0;
                        }
                        break;
                    }
            }
        }
    }
}
