using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    Vector2 move = new Vector2();
    Rigidbody2D rigidbody2D;

    public GameObject player;

    float x;
    float y;
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
        if(player.GetComponent<playerStat>().playerDie == false) 
        {
            UpdateState();
        }


        
    }

    private void FixedUpdate() 
    {
        if (player.GetComponent<playerStat>().playerDie == false)
        {
            MoveCharacter();
        }
    }

    public void MoveCharacter()
    {
        
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

      
        

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
       

       

        if (move.x != 0 || move.y != 0)
        {
            animator.SetFloat("lastxdir", move.x);
            animator.SetFloat("lastydir", move.y);

            x = animator.GetFloat("lastxdir");
            y = animator.GetFloat("lastydir");
           
        }
       // Debug.Log(x);
       //  Debug.Log(y);



    }
}
