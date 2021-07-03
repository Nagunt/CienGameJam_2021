using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MyCharacter
{
    private bool IsAttack = false;
    private float AttackTime = 0f;
    private float AttackDelay = 0.6f;
    public Transform Attack;
    public GameObject AttackOut;
    public GameObject AttackInner;
    public Transform PlayerTransform;
    public Transform TargetTransform;
    private float DashTime = 0f;
    private float DashDelay = 1.5f;
    private bool IsDash = false;
    private bool DashWait = false;

    // Update is called once per frame
    void Update()
    {
        
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.S))
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
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
            Attack.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            Attack.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (inputVector.x == 0)
        {
            Stop();
        }
        Move(inputVector);

        if (Input.GetMouseButtonDown(0) && IsAttack == false)
        {
            IsAttack = true;
            AttackOut.SetActive(true);
            AttackInner.SetActive(true);
            StartCoroutine(_Routine());

            IEnumerator _Routine()
            {
                yield return new WaitForSeconds(0.07f);
                AttackOut.SetActive(false);
                AttackInner.SetActive(false);
            }

        }
        /*if (Input.GetMouseButtonDown(1))
        {
            rb2D = GetComponent<Rigidbody2D>();
            //PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, PlayerTransform.position + new Vector3(3, 0, 0), 1);
            rb2D.AddForce(new Vector2(inputVector.x < 0 ? -3000f : 3000f, 0));
        }*/

        if (IsAttack == true && AttackTime < AttackDelay)
        {
            AttackTime += Time.deltaTime;
        }
        else
        {
            AttackTime = 0f;
            IsAttack = false;
        }

        if (Input.GetMouseButton(1) && DashWait == false)
        {
            rb2D.velocity = new Vector2(inputVector.x * speed * 600f, 0);
            PlayerTransform.Translate(new Vector2(inputVector.x > 0 ? Time.deltaTime * speed * 6.66f : Time.deltaTime * speed * -6.66f, 0));
            //TargetTransform.position = new Vector2(inputVector.x > 0 ? PlayerTransform.position.x + 3f : PlayerTransform.position.x - 3f, PlayerTransform.position.y);
            //PlayerTransform.position = Vector2.MoveTowards(PlayerTransform.position, TargetTransform.position, speed * Time.deltaTime * 6.66f);
            rb2D.isKinematic = true;
            IsDash = true;
        }
        if (IsDash == true && DashTime < DashDelay)
        {
            DashTime += Time.deltaTime;
            if (DashTime > 0.15f)
            {
                DashWait = true;
                rb2D.isKinematic = false;
            }
        }
        else
        {
            DashTime = 0f;
            IsDash = false;
            DashWait = false;
        }
    }
}
