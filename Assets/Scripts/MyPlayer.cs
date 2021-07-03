using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MyCharacter
{

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                inputVector.y = -1;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                inputVector.y = 1;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x += 1;
        }
        if(inputVector.x == 0)
        {
            Stop();
        }
        Move(inputVector);
    }
}
