using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeKing : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public GameObject enemyProjectile;
    public Animator animator;
    public bool onWake = false;
    public bool onTrigger = false;
    public bool attackMotionDone = true;
    public bool onFlash = false;
    public bool IsDie = false;
    public Vector3 targetPos;
    public bool isDash;
    public int nowHp;
    public int damaged;
    public Vector3 MoveTowardsVector;
    public int waitCount;

    private enum State
    {
        idle,
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
        fsm = new FSM(new sleepState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();
        waitCount = 0;
        onFlash = false;
        IsDie = false;
    }


    private void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.000001f, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.000001f, 0);
        if (this.damaged != 0)
        {
            if (!onFlash)
            {
                nowHp -= this.damaged;
                onFlash = true;
                StartCoroutine(FlashWhite());
            }
                this.damaged = 0;
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
                if (waitCount == 3)
                {
                    ChangeState(State.attack);                 
                }
                break;
            case State.attack:

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

    private void waitTimer()
    {
        waitCount++;
    }

    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 1);
                break;
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


    public void AttackDash1()
    {
        targetPos = player.transform.position;
        MoveTowardsVector = Vector3.Normalize(targetPos - this.transform.position);
 
    }
    public void AttackDash2()
    {
        float angle = Mathf.Atan2(targetPos.y - this.transform.position.y, targetPos.x - this.transform.position.x) * Mathf.Rad2Deg;

        if (angle >= -90 && angle < 90)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        isDash = true;
        StartCoroutine(dash());
    }
    public void AttackDash3()
    {
        isDash = false;
    }

    public void AttackDash4()
    {
        int chance = Random.Range(0, 2);
        switch (chance)
        {
            case 0:
                animator.SetInteger("Pattern", 1);
                break;

            case 1:
                animator.SetInteger("Pattern", 2);
                break;
        }

    }


    public void AttackShoot1()
    {

        enemyProjectile = Instantiate(monsterStat.projectile);
        enemyProjectile.name = "EnemyArrow";

        enemyProjectile.transform.position = this.transform.position;
        enemyProjectile.gameObject.GetComponent<enemyArrow>().target = player;
        enemyProjectile.gameObject.GetComponent<enemyArrow>().dmg = monsterStat.damage;
        enemyProjectile.gameObject.GetComponent<enemyArrow>().speed = monsterStat.projectileSpeed;

    }

    public void AttackShoot2()
    {
        int chance = Random.Range(0, 2);
        switch (chance)
        {
            case 0:
                animator.SetInteger("Pattern", 1);
                break;

            case 1:
                animator.SetInteger("Pattern", 2);
                break;
        }
    }


    IEnumerator dash()
    {
        while (isDash)
        {
            yield return new WaitForEndOfFrame();
            // this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 0.05f);
            if (this.curState != State.die)
            {
                transform.position += MoveTowardsVector * 10f * Time.deltaTime;
            }
            if (isDash == false)
            {
                yield break;
            }
        }
        if (isDash == false)
        {
            yield break;
        }
    }
    public class sleepState : BaseState
    {
        public sleepState(enemy enemy, GameObject player) : base(enemy, player) { }

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