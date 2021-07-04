using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MyEnemy_BWolf : MyEnemy
{
    [Header("- Attack")]
    [SerializeField] protected Transform attack;
    [SerializeField] protected MyEnemyAttack attack_1;
    protected override void Init()
    {
        hp = 4;
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
                // 플레이어 몸통 히트 대미지
                col2D.gameObject.GetComponentInParent<MyPlayer>().HP -= 1;
            }
        }
    }
    private void Update()
    {
        animator.SetInteger("HP", HP);
        animator.SetFloat("xSpeed", rb2D.velocity.x);
        animator.SetBool("IsGround", IsGround);
        if (isAlive)
        {
            detect.Clear();
            switch (phase)
            {
                case MyEnemyPhase.IDLE:
                    {
                        if(lastPhase != phase)
                        {
                            // 페이즈 변경이 일어난 프레임에만 실행
                            Stop();
                            
                            if (patrolArea.Length > 0) // 패트롤이 있을 때
                            {
                                patrolIndex = 0;
                                patrolPos = new Vector2[patrolArea.Length];
                                for (int i = 0; i < patrolPos.Length; ++i)
                                {
                                    RaycastHit2D borderCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.15f), patrolArea[i].localPosition, patrolArea[i].localPosition.magnitude, LayerMask.GetMask("Border"));
                                    if (borderCheck.collider != null)
                                    {
                                        patrolPos[i] = new Vector2(borderCheck.point.x - col2D.size.x * .5f * (patrolArea[i].localPosition.x > 0 ? 1 : -1), borderCheck.point.y - 0.15f);
                                    }
                                    else
                                    {
                                        patrolPos[i] = patrolArea[i].position;
                                    }

                                    RaycastHit2D groundCheckLeft = Physics2D.Raycast(new Vector2(patrolPos[i].x - col2D.size.x * .5f, patrolPos[i].y), Vector2.down, 0.15f, LayerMask.GetMask("Terrain"));
                                    RaycastHit2D groundCheckRight = Physics2D.Raycast(new Vector2(patrolPos[i].x + col2D.size.x * .5f, patrolPos[i].y), Vector2.down, 0.15f, LayerMask.GetMask("Terrain"));

                                    if(groundCheckLeft.collider == null || groundCheckRight.collider == null)
                                    {
                                        RaycastHit2D groundCheck = Physics2D.Raycast(
                                            new Vector2(patrolPos[i].x + col2D.size.x * .5f * (patrolArea[i].localPosition.x > 0 ? 1 : -1), patrolPos[i].y - 0.15f),
                                            (patrolArea[i].localPosition.x > 0 ? Vector2.left : Vector2.right),
                                            Mathf.Abs(patrolPos[i].x - transform.position.x),
                                            LayerMask.GetMask("Terrain"));
                                        if(groundCheck.collider != null)
                                        {
                                            patrolPos[i] = new Vector2(groundCheck.point.x - col2D.size.x * .5f * (patrolArea[i].localPosition.x > 0 ? 1 : -1), groundCheck.point.y + 0.15f);
                                        }
                                        else
                                        {
                                            patrolPos[i] = transform.position;
                                        }
                                    }
                                }
                            }
                            lastPhase = phase;
                        }
                        if (Physics2D.OverlapCollider(detectArea, contactFilter2D, detect) > 0)
                        {
                            Vector2 nowPos = new Vector2(transform.position.x, transform.position.y);
                            Vector2 targetPos = new Vector2(detect[0].transform.position.x, detect[0].transform.position.y);
                            RaycastHit2D check = Physics2D.Raycast(nowPos, targetPos - nowPos, (targetPos - nowPos).magnitude, LayerMask.GetMask("Border"));
                            if(check.collider == null)
                            {
                                target = detect[0].transform;
                                phase = MyEnemyPhase.MOVE;
                            }
                        }

                        if (phase == MyEnemyPhase.IDLE)
                        {
                            if(patrolArea.Length > 0)
                            {
                                if(transform.position.x > patrolPos[patrolIndex].x + minimumDistance || transform.position.x < patrolPos[patrolIndex].x - minimumDistance)
                                {
                                    Move(patrolPos[patrolIndex] - (Vector2)transform.position);
                                    model.flipX = patrolPos[patrolIndex].x > transform.position.x;
                                }
                                else
                                {
                                    if(++patrolIndex >= patrolPos.Length)
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
                            // 페이즈 변경이 일어난 프레임에만 실행
                            Stop();
                            lastPhase = phase;
                        }

                        // 페이즈 변경 조건 판정
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
                        // 페이즈 변경 조건 판정 끝

                        // 페이즈 실행 시
                        if (phase == MyEnemyPhase.MOVE)
                        {
                            if(target != null)
                            {
                                animator.SetBool("IsAttack", false);
                                Move(target.position - transform.position);
                                model.flipX = target.position.x > transform.position.x;
                            }
                        }
                        else
                        {
                            // 페이즈 종료 시

                        }
                        break;
                    }
                case MyEnemyPhase.ATTACK:
                    {
                        if (lastPhase != phase)
                        {
                            // 페이즈 변경이 일어난 프레임에만 실행
                            delay = 0f;
                            Stop();
                            attack.localRotation = Quaternion.Euler(0, target.position.x > transform.position.x ? 180 : 0, 0);
                            attackData = attack_1;
                            attackPhase = MyEnemyAttackPhase.BEFORE;
                            model.flipX = target.position.x > transform.position.x;
                            animator.SetBool("IsAttack", true);
                            lastPhase = phase;
                        }

                        animator.SetInteger("AttackPhase", (int)attackPhase);

                        switch (attackPhase)
                        {
                            case MyEnemyAttackPhase.BEFORE:
                                {
                                    if(delay < attackData.attackBeforeDelay)
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
                                        animator.SetBool("IsAttack", false);
                                        phase = MyEnemyPhase.MOVE;
                                        delay = 0;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
            }
        }
    }
}
