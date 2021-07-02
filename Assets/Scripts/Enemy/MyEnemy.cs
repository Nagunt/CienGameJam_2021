using UnityEngine;

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
    private int attack;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Dead()
    {

    }
    
    void OnHPChanged()
    {
        if (hp < 0)
        {
            Dead();
        }
    }
}
