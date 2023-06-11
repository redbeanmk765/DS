using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_Archer : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public GameObject enemyArrow;
    public Animator animator;
    public bool onWake = false;
    public bool onTrigger = false;
    public bool attackMotionDone = true;
    public bool onFlash = false;
    public bool IsDie = false;
    public Vector3 targetPos;
    public int nowHp;
    public int damaged;
    public Vector3 MoveTowardsVector;

    private enum State
    {
        //sleep,
        //wake,
        idle,
        //move,
        attack,
        die
    }

    private State curState;
    private FSM fsm;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        curState = State.idle;
        fsm = new FSM(new IdleState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();
        onFlash = false;
        IsDie = false;
    }


    private void Update()
    {

        if (this.damaged != 0)
        {
            nowHp -= this.damaged;
            this.damaged = 0;
            onFlash = true;
            StartCoroutine(FlashWhite());
        }
        if (nowHp <= 0)
        {
            if (IsDie == false)
            {
                this.curState = State.die;
                ChangeState(State.die);
                IsDie = true;
            }

        }

        switch (curState)
        {
            case State.idle:

                if (CanSeePlayer())
                {

                    if (CanAttackPlayer())
                    {
                        ChangeState(State.attack);
                    }

                }
                break;
            case State.attack:
                if (attackMotionDone)
                {
                    if (CanSeePlayer())
                    {
                        if (!CanAttackPlayer())
                        {
                            ChangeState(State.idle);
                        }

                    }
                    else
                    {
                        ChangeState(State.idle);
                    }
                }
                break;
            case State.die:
                if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    Destroy(this.gameObject);
                }
                break;

        }

        fsm.UpdateState();
    }

    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.flashMaterial;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;

            if (onFlash == false)
            {
                yield break;
            }
            onFlash = false;


        }
        if (onFlash == false)
        {
            yield break;
        }
    }


    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            //case State.wake:
            //    fsm.ChangeState(new wakeState(this, player));
            //    animator.SetInteger("State", 1);
            //    break;
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 1);
                break;
            //case State.move:
            //    fsm.ChangeState(new MoveState(this, player));
            //    animator.SetInteger("State", 3);
            //    break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 3);
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 10)
        {   
            return true;

        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 10)
        {

            return true;
        }
        else
            return false;
        //  사정거리 체크 구현
    }

    public void AttackShoot1()
    {     
        attackMotionDone = false;
    }
    public void AttackShoot2()
    {
        enemyArrow = Instantiate(monsterStat.projectile);
        enemyArrow.name = "EnemyArrow";

        enemyArrow.transform.position = this.transform.position;
        enemyArrow.gameObject.GetComponent<enemyArrow>().target = player;
        enemyArrow.gameObject.GetComponent<enemyArrow>().dmg = monsterStat.damage; 
    }
    public void AttackShoot3()
    {    
        attackMotionDone = true;
    }

   
    public class IdleState : BaseState
    {
        public IdleState(enemy enemy, GameObject player) : base(enemy, player) { }
        public override void OnStateEnter()
        {
        }

        public override void OnStateUpdate()
        {
            float angle = Mathf.Atan2(curPlayer.transform.position.y - curEnemy.transform.position.y, curPlayer.transform.position.x - curEnemy.transform.position.x) * Mathf.Rad2Deg;

            if (angle >= -90 && angle < 90)
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
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
          
            float angle = Mathf.Atan2(curPlayer.transform.position.y - curEnemy.transform.position.y, curPlayer.transform.position.x - curEnemy.transform.position.x) * Mathf.Rad2Deg;

            if (angle >= -90 && angle < 90)
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
        }

        public override void OnStateExit()
        {
        }
    }

    public class DieState : BaseState
    {
        public DieState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {

        }

        public override void OnStateUpdate()
        {

            curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.deltaTime));

        }

        public override void OnStateExit()
        {
        }
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (this.curState != State.die)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                onTrigger = true;
                col.gameObject.GetComponent<playerStat>().damaged = monsterStat.damage;
                colPlayer = col.gameObject;
                StartCoroutine(WaitForDamage());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            // this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
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
            colPlayer.gameObject.GetComponent<playerStat>().damaged = monsterStat.damage;
        }
        if (onTrigger == false)
        {
            yield break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (this.curState != State.die)
        {

            if (col.CompareTag("attack"))
            {

                this.damaged += col.gameObject.GetComponent<weaponStat>().dmg;

            }
        }
    }


}