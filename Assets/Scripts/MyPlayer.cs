using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        if (Input.GetAxis("Jump") > 0)
        {
            Debug.Log("Jump");
            rb2D.velocity += Vector2.up * jumpSpeed;
        }
    }
}
