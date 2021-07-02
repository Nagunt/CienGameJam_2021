using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    [Header("- Model")]
    [SerializeField] protected SpriteRenderer model;

    [Header("- Physics")]
    [SerializeField] protected Collider2D col2D;
    [SerializeField] protected Rigidbody2D rb2D;

    [Header("- Movement")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpSpeed;

    [Header("- Ground Check")]
    [SerializeField]
    private bool isGround = false;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private ContactFilter2D groundFilter;

    private List<RaycastHit2D> hits = new List<RaycastHit2D>();

    public bool IsGround
    {
        get
        {
            return isGround;
        }
        private set
        {
            isGround = value;
        }
    }

    private void FixedUpdate()
    {
        Collider2D groundCol2D = null;
        for(int i = 0; i < MyStageManager.Instance.terrainInfo.Count; ++i)
        {
            MyTerrain terrain = MyStageManager.Instance.terrainInfo[i];
            Physics2D.IgnoreCollision(col2D, terrain.GetCollider2D(), transform.position.y < terrain.Depth);
            if(transform.position.y >= terrain.Depth && transform.position.y - terrain.Depth < 1)
            {
                groundCol2D = terrain.GetCollider2D();
            }
        }

        if (groundCol2D != null)
        {
            hits.Clear();
            Physics2D.Raycast(transform.position, Vector2.down, groundFilter, hits, groundDistance);

            if(hits.Count > 0)
            {
                for (int i = 0; i < hits.Count; ++i)
                {
                    if(hits[i].collider == groundCol2D)
                    {
                        IsGround = true;
                        return;
                    }
                }
            }
        }
        IsGround = false;
    }
}
