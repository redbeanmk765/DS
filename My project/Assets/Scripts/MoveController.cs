using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    Vector2 move = new Vector2();
    Rigidbody2D rigidbody2D;
    Vector3 rotation = new Vector3();

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void FixedUpdate() 
    {
        MoveCharacter();
    }

    public void MoveCharacter()
    {
        
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        if(move.x > 0 )
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }

        if (move.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        if (move.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        if (move.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,0,180);
        }


        move.Normalize();

        rigidbody2D.velocity = move * moveSpeed;
    }

    public void UpdateState()
    {
        if (Mathf.Approximately(move.x, 0) && Mathf.Approximately(move.y, 0))
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }
        animator.SetFloat("xdir", move.x);
        animator.SetFloat("ydir", move.y);
    }
}
