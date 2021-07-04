using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum MyEnemyPhase
{
    None = 0,
    IDLE,
    MOVE,
    ATTACK,
    RUN,
    SWAY
}


public class MyEnemy : MyCharacter
{
    protected Sequence sequence;

    protected int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            if (!sequence.IsActive())
            {

                sequence = DOTween.Sequence();
                sequence.OnStart(() =>
                {
                    animator.SetBool("IsHit", true);
                }).
                AppendInterval(0.5f).
                AppendCallback(() =>
                {
                    animator.SetBool("IsHit", false);
                });
            }
            hp = value;
            if (hp <= 0)
            {
                if (isAlive)
                {
                    Dead();
                }
            }
        }
    }

    protected bool isAlive;


    [Header("- AI")]
    protected MyEnemyPhase lastPhase = MyEnemyPhase.None;
    [SerializeField] protected MyEnemyPhase phase;
    [SerializeField] protected Transform target;
    [Header("- Animator")]
    [SerializeField] protected Animator animator;
    [Header("- Area")]
    [SerializeField] protected ContactFilter2D contactFilter2D;
    [SerializeField] protected Transform[] patrolArea;
    [SerializeField] protected Collider2D detectArea;
    [SerializeField] protected Collider2D attackDetectArea;

    protected MyEnemyAttack attackData;
    protected List<Collider2D> detect;

    protected float delay = 0f;
    protected int patrolIndex;
    protected float minimumDistance = 0.1f;
    protected Vector2[] patrolPos;
    protected MyEnemyAttackPhase attackPhase;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        isAlive = true;
        MyGameManager.Instance.EnemyCount++;
    }

    protected virtual void Dead()
    {
        model.DOColor(new Color(1, 1, 1, 0), 1f);
        Debug.Log(name + " Dead");
        isAlive = false;
        MyGameManager.Instance.EnemyCount--;
        
        rb2D.isKinematic = true;
        col2D.enabled = false;
    }

}
