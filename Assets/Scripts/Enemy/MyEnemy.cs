using System.Collections.Generic;
using UnityEngine;

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
    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            OnHPChanged();
        }
    }

    protected bool isAlive;


    [Header("- AI")]
    protected MyEnemyPhase lastPhase = MyEnemyPhase.None;
    [SerializeField] protected MyEnemyPhase phase;
    [SerializeField] protected Transform target;
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

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        isAlive = true;
    }

    protected virtual void Dead()
    {
        Debug.Log(name + " Dead");
        isAlive = false;
    }
    
    void OnHPChanged()
    {
        if (hp <= 0)
        {
            Dead();
        }
    }
}
