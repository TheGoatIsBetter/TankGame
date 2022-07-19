using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_Simple : AIController
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
                //do state
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
                if (IsTimePassed(1))
                {
                    ChangeState(AIStates.Flee);
                }
                break;
            case AIStates.Flee:
                DoFleeState();
                break;
            case AIStates.Attack:
                DoAttackState();

                if (CanHear(AITarget))
                {
                    ChangeState(AIStates.Idle);
                }

                break;
            case AIStates.Patrol:
                DoPatrolState();
                //check conditions
                //stay here forever
                break;
            case AIStates.Chase:
                //do state
                DoChaseState();
                //check for change state
                if (IsTimePassed(1))
                {
                    ChangeState(AIStates.Idle);
                }
                break;
        }
    }

}
