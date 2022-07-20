using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_SentryChaser : AIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //start idle
        ChangeState(AIStates.Idle);

        //target start w/dummy
        AITarget = dummyTarget;
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
                //do state
                DoIdleState();
                //check for change state
                if (IsTimePassed(2))
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                break;
            case AIStates.ChooseTarget:
                DoChooseTargetState();
                if (CanSee(AITarget, fieldOfView))
                {
                    ChangeState(AIStates.Sentry);
                }
                break;
            case AIStates.Sentry:
                DoSentryState();
                if (CanHear(AITarget))
                {
                    ChangeState(AIStates.Attack);
                }
                if (CanSee(AITarget, fieldOfView) == false && IsTimePassed(3))
                {
                    ChangeState(AIStates.Idle);
                }
                break;
            case AIStates.Attack:
                DoAttackState();
                if (IsTimePassed(5))
                {
                    ChangeState(AIStates.Sentry);
                }
                break;

        }
    }

}
