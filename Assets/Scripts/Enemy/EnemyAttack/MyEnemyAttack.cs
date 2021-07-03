using UnityEngine;

public enum MyEnemyAttackPhase
{
    None = 0,
    BEFORE,
    ATTACK,
    AFTER
}

public class MyEnemyAttack : MonoBehaviour
{
    public float attackBeforeDelay;
    public float attackDelay;
    public float attackAfterDelay;

    public virtual void Execute(Vector3 _target)
    {

    }
}
