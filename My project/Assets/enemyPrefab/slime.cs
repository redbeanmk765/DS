using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : enemy
{
    private enum State
    {
        Idle,
        Move,
        Attack
    }

    private State curState;
    private FSM fsm;

    private void Start()
    {
        curState = State.Idle;
        fsm = new FSM(new IdleState(this));
    }

    private void Update()
    {
        switch (curState)
        {
            case State.Idle:
                if (CanSeePlayer())
                {
                    if (CanAttackPlayer())
                        ChangeState(State.Attack);
                    else
                        ChangeState(State.Move);
                }
                break;
            case State.Move:
                if (CanSeePlayer())
                {
                    if (CanAttackPlayer())
                    {
                        ChangeState(State.Attack);
                    }
                }
                else
                {
                    ChangeState(State.Idle);
                }
                break;
            case State.Attack:
                if (CanSeePlayer())
                {
                    if (!CanAttackPlayer())
                    {
                        ChangeState(State.Move);
                    }
                }
                else
                {
                    ChangeState(State.Idle);
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
            case State.Idle:
                fsm.ChangeState(new IdleState(this));
                break;
            case State.Move:
                fsm.ChangeState(new MoveState(this));
                break;
            case State.Attack:
                fsm.ChangeState(new AttackState(this));
                break;
        }
    }

    private bool CanSeePlayer()
    {
        return true;
        // TODO:: 플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        return true;
        // TODO:: 사정거리 체크 구현
    }
}