using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    [Header("- Model")]
    [SerializeField] protected SpriteRenderer model;

    [Header("- Physics")]
    [SerializeField] protected BoxCollider2D col2D;
    [SerializeField] protected Rigidbody2D rb2D;

    [Header("- Movement")]
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpSpeed;

    [Header("- Ground Check")]
    [SerializeField]
    private bool isGround = false;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private ContactFilter2D groundFilter;

    private Vector2 moveVector = Vector2.zero;
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
        bool checker = false;
        if (groundCol2D != null)
        {
            hits.Clear();
            //Physics2D.Raycast(transform.position, Vector2.down, groundFilter, hits, groundDistance);
            Physics2D.BoxCast(transform.position, new Vector2(col2D.size.x, groundDistance), 0, Vector2.down, groundFilter, hits, groundDistance);
            if(hits.Count > 0)
            {
                for (int i = 0; i < hits.Count; ++i)
                {
                    if(hits[i].collider == groundCol2D)
                    {
                        checker = true;
                        break;
                    }
                }
            }
        }
        
        IsGround = checker;

        Vector2 direction = moveVector.normalized;
        model.flipX = direction.x < 0;
        if (IsGround)
        {
            if (direction.y > 0.5f)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 1 * jumpSpeed);

            }
            else if (direction.y < -0.5f)
            {
                hits.Clear();
                Physics2D.Raycast(transform.position, Vector2.down, groundFilter, hits);
                int count = 0;
                for(int i = 0; i < hits.Count; ++i)
                {
                    if(hits[i].collider.TryGetComponent(out MyTerrain terrain))
                    {
                        if(transform.position.y < terrain.Depth)
                        {
                            continue;
                        }
                        count++;
                    }
                }
                if (count > 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z);
                }
            }
        }

        rb2D.velocity = new Vector2(direction.x * speed, rb2D.velocity.y);
        moveVector = Vector2.zero;
    }

    void OnDrawGizmos()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, new Vector2(col2D.size.x, groundDistance), 0, Vector2.down, groundDistance);

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, new Vector2(col2D.size.x, groundDistance));
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector2.down * groundDistance);
        }
    }

    public void Move(Vector2 _direction)
    {
        moveVector = _direction;
    }

    public void Stop()
    {
        moveVector = Vector2.zero;
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
    }
}
