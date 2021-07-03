using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MyMovingPlatform : MonoBehaviour
{
    [Header("- Physics")]
    [SerializeField] protected BoxCollider2D col2D;
    [SerializeField] protected Rigidbody2D rb2D;
    [Header("- Movement")]
    [SerializeField] protected float speed;
    [SerializeField] protected Transform[] path;
    protected int pathIndex;
    protected Vector3[] pathInfo;
    private float minDistance = 0.1f;
    private Vector2 moveVector = Vector2.zero;

    public float Depth
    {
        get
        {
            return transform.position.y + 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pathInfo = new Vector3[path.Length];
        for(int i = 0; i < pathInfo.Length; ++i)
        {
            pathInfo[i] = path[i].position;
        }
    }

    public Collider2D GetCollider2D()
    {
        return col2D;
    }

    private void Update()
    {
        moveVector = Vector2.zero;
        if (transform.position.x > pathInfo[pathIndex].x + minDistance || transform.position.x < pathInfo[pathIndex].x - minDistance)
        {
            moveVector.x = pathInfo[pathIndex].x - transform.position.x;
        }
        if(transform.position.y > pathInfo[pathIndex].y + minDistance || transform.position.y < pathInfo[pathIndex].y - minDistance)
        {
            moveVector.y = pathInfo[pathIndex].y - transform.position.y;
        }
        if(
            Mathf.Abs(transform.position.x - pathInfo[pathIndex].x) < minDistance && 
            Mathf.Abs(transform.position.y - pathInfo[pathIndex].y) < minDistance)
        {
            rb2D.velocity = Vector2.zero;
            if (++pathIndex >= pathInfo.Length)
            {
                pathIndex = 0;
            }
        }
    }
    
    private void FixedUpdate()
    {
        rb2D.velocity = moveVector.normalized * speed;
        moveVector = Vector2.zero;
    }

}
