using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Patroller : AIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //start idle
        ChangeState(AIStates.Idle);

        //target start w/player one
        AITarget = GameManager.instance.players[0].pawn.gameObject;
    }

    // Update is called once per frame
    protected override void Update()
    {
        MakeDecisions();
    }

    public override void MakeDecisions()
    {
        if (pawn == null) return; //prevent null reference errors

        //FSM
        //based on current state
        switch (currentState)
        {
            case AIStates.Idle:
                DoIdleState();
                //check for change state
                if (IsTimePassed(2))
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                break;
            case AIStates.ChooseTarget:
                //do state
                DoChooseTargetState();
                //check for change state
                if (IsTimePassed(2))
                {
                    ChangeState(AIStates.Patrol);
                }
                break;
            case AIStates.Patrol:
                DoPatrolState();
                if (CanSee(AITarget, fieldOfView) || CanHear(AITarget))
                {
                    ChangeState(AIStates.Sentry);
                }
                break;
            case AIStates.Sentry:
                DoSentryState();
                if (IsTimePassed(1))
                {
                    ChangeState(AIStates.Attack);
                }
                break;
            case AIStates.Attack:
                DoAttackState();
                if (!CanHear(AITarget) && !CanSee(AITarget, fieldOfView) && IsTimePassed(3))
                {
                    ChangeState(AIStates.Patrol);
                }
                break;

        }
    }

}
