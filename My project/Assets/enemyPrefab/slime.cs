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
        curState = State.idle;
        fsm = new FSM(new IdleState(this));
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
    }

 
    private void Update()
    {
        switch (curState)
        {
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
        Vector2.Distance()
        return true;
        // TODO:: 플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        return true;
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