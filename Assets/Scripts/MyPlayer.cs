using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MyCharacter
{
    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2D.velocity.y);
        if (IsGround) 
        { 
            if (Input.GetKey(KeyCode.Space))
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 1 * jumpSpeed);
            }
        }
    }
}
