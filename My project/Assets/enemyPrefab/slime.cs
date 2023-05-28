using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public Animator animator;
    public bool onTrigger = false;
   // public float angle;
   // Vector2 playerPos, enemyPos;
    private enum State
    {
        sleep,
        wake,
        idle,
        move,
        attack
    }

    private State curState;
    private FSM fsm;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        curState = State.sleep;
        fsm = new FSM(new sleepState(this,player));

      //  playerPos = player.transform.position;
       // enemyPos = this.transform.position;

        animator = GetComponent<Animator>();
        Debug.Log(monsterStat.damage);
    }


    private void Update()
    {
        //angle = Mathf.Atan2(playerPos.y - playerPos.y, enemyPos.x - enemyPos.x) * Mathf.Rad2Deg;
        switch (curState)
        {
            case State.sleep:
                if (CanSeePlayer())
                {
                    ChangeState(State.wake);
                }
                break;
            case State.wake:
                {
                    Invoke("EnterIdle", 2);
                    break;
                }
            case State.idle:
               
                if (CanSeePlayer())
                {
                  
                    if (CanAttackPlayer())
                        ChangeState(State.attack);
                    else
                        ChangeState(State.move);
                }
                break;
            case State.move:
                if (CanSeePlayer())
                {
                    
                    if (CanAttackPlayer())
                    {
                        ChangeState(State.attack);
                    }
                }
                else
                {
                    ChangeState(State.idle);
                }
                break;
            case State.attack:
                if (CanSeePlayer())
                {
                    if (!CanAttackPlayer())
                    {
                        ChangeState(State.move);
                    }
                   
                }
                else
                {
                    ChangeState(State.idle);
                }
                break;
        }

        fsm.UpdateState();
    }

    private void EnterIdle() 
    {
        ChangeState(State.idle);
    }

    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.wake:
                fsm.ChangeState(new wakeState(this,player));
                animator.SetInteger("State", 1);
                break;
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.move:
                fsm.ChangeState(new MoveState(this, player));
                animator.SetInteger("State", 3);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 4);
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 4)
        {

            return true;
            
        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 2)
        {

            return true;
        }
        else
            return false;
        //  사정거리 체크 구현
    }

    

    public class sleepState : BaseState
    {
        public sleepState(enemy enemy, GameObject player) : base(enemy,player) { }

        public override void OnStateEnter()
        {           
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }
    public class wakeState : BaseState
    {
        public wakeState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {

        }

        public override void OnStateUpdate()
        {

        }

        public override void OnStateExit()
        {
        }
    }
    public class IdleState : BaseState
    {
        public IdleState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {

        }

        public override void OnStateExit()
        {
        }
    }
    
    public class MoveState : BaseState
    {
        
        public MoveState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {
            float angle = Mathf.Atan2(curPlayer.transform.position.y - curEnemy.transform.position.y, curEnemy.transform.position.x - curEnemy.transform.position.x) * Mathf.Rad2Deg;
        }

        public override void OnStateUpdate()
        {         
            curEnemy.transform.position = Vector3.MoveTowards(curEnemy.transform.position, curPlayer.transform.position, curEnemy.monsterStat.moveSpeed * Time.deltaTime);
            
        }

        public override void OnStateExit()
        {
        }
    }

    public class AttackState : BaseState
    {
        public AttackState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
       
 
        
        if (col.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            onTrigger = true;
            col.gameObject.GetComponent<playerStat>().damage = monsterStat.damage;
            colPlayer = col.gameObject;
            StartCoroutine(WaitForDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            onTrigger = false;
        }
    }

    IEnumerator WaitForDamage()
    {
        while (onTrigger)
        {
            yield return new WaitForSeconds(1.0f);

            if (onTrigger == false)
            {
                yield break;
            }
            colPlayer.gameObject.GetComponent<playerStat>().damage = monsterStat.damage;
        }
        if (onTrigger == false)
        {
            yield break;
        }
    }

}