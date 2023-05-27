using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject[] players;

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
        Debug.Log("START");
        curState = State.idle;
        fsm = new FSM(new IdleState(this));
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
    }

 
    private void Update()
    {
        Debug.Log("Update");
        switch (curState)
        {
            case State.idle:
                Debug.Log("IDLE");
                if (CanSeePlayer())
                {
                    Debug.Log("NEAR");
                    if (CanAttackPlayer())
                        ChangeState(State.attack);
                    else
                        ChangeState(State.move);
                }
                break;
            case State.move:
                if (CanSeePlayer())
                {
                    Debug.Log("MOVE");
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
                    Debug.Log("ATTACK");
                }
                else
                {
                    ChangeState(State.idle);
                }
                break;
        }

        fsm.UpdateState();
    }

    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.idle:
                fsm.ChangeState(new IdleState(this));
                break;
            case State.move:
                fsm.ChangeState(new MoveState(this));
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this));
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 5)
        {
           // Debug.Log("NEAR");
            return true;
            
        }
        else
            return false;
        // TODO:: 플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 2)
        {
           // Debug.Log("ATTACK NEAR");
            return true;
        }
        else
            return false;
        // TODO:: 사정거리 체크 구현
    }

    public class IdleState : BaseState
    {
        public IdleState(enemy enemy) : base(enemy) { }

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            Debug.Log("IsIDLE");
        }

        public override void OnStateExit()
        {
        }
    }

    public class MoveState : BaseState
    {
        public MoveState(enemy enemy) : base(enemy) { }

        public override void OnStateEnter()
        {
            Debug.Log("EnterMove");
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }

    public class AttackState : BaseState
    {
        public AttackState(enemy enemy) : base(enemy) { }

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


}