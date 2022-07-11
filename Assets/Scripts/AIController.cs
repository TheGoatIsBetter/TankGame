using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIStates { Idle, Chase, Attack, Flee, ChooseTarget, Patrol};
    [SerializeField] protected AIStates currentState;
    protected float timeEnteredCurrentState;
    [SerializeField] protected GameObject AITarget;
    [SerializeField] protected List<Transform> waypoints;
    protected int currentWaypoint;
    [SerializeField] protected float hearingRadius;
    [SerializeField] protected float fieldOfView;
    [SerializeField] protected float viewDistance;

    protected override void Start()
    {
        //add itself to list of ais
        GameManager.instance.ais.Add(this);
    }

    public virtual void ChangeState(AIStates newState)
    {
        //remember change state time
        timeEnteredCurrentState = Time.time;
        //change state
        currentState = newState;
    }

    public virtual void DoIdleState()
    {
        //do nothing
    }

    public virtual void DoChaseState()
    {
        //TODO: Add max speed to pawns and set this AI's to max


        //chase the player
        Chase(AITarget);
    }

    public virtual void DoChooseTargetState()
    {
        AITarget = GameManager.instance.players[0].pawn.gameObject;
    }

    public virtual void DoAttackState()
    {
        //Chase the player

        //shoot at them
    }

    public virtual void DoPatrolState()
    {
        //create temp target loc
        Vector3 seekPos = waypoints[currentWaypoint].position;

        //adjust temp loc so y is same y as pawn
        seekPos = new Vector3(seekPos.x, 
                                         pawn.transform.position.y,
                                         seekPos.z);
        //move to current waypoint
        Chase(seekPos);
       
        //increment current waypoint
        if (Vector3.Distance((pawn.transform.position), seekPos) <= 1)
        {
            currentWaypoint++;
        }

        //if last waypoint set next to first
        if (currentWaypoint >= waypoints.Count)
        {
            currentWaypoint = 0;
        }
    }

    public virtual void Chase(Vector3 chaseTarget)
    {
        //turn towards target
        pawn.TurnTowards(chaseTarget);

        //move forward
        pawn.MoveForward();
    }

    public virtual void Chase(Transform chaseTarget)
    {
        Chase(chaseTarget.position);
    }

    public virtual void Chase(GameObject chaseTarget)
    {
        if (chaseTarget != null)
        {
            Chase(chaseTarget.transform.position);
        }
    }

    public virtual bool IsTimePassed (float amountOfTime)
    {
        if (Time.time - timeEnteredCurrentState >= amountOfTime)
        {
            return true;
        }
        return false;
    }

    public virtual bool CanSee( GameObject target)
    {
        //check if in FoV
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        float angleToTarget = Vector3.Angle(pawn.transform.forward, vectorToTarget);
        if(angleToTarget > fieldOfView)
        {
            return false;
        }

        //check if in LoS
        Ray tempRay = new Ray(pawn.transform.position, vectorToTarget);
        RaycastHit hitInfo;
        if (Physics.Raycast(tempRay, out hitInfo, viewDistance))
        {
            //check if hit
            if(hitInfo.collider.gameObject == target)
            {
                //then can see
                return true;
            }
        }

        //if this far, not seen
        return false;
    }


    //can the AI hear a target
    public virtual bool CanHear( GameObject target)
    {
        //get noisemaker component
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        //if it does not exist
        if (targetNoiseMaker == null)
        {
            //then not making sound (false)
            return false;
        }
        else
        {
            // if distance between objects is less than sum of two radii
            float sumOfRadii = targetNoiseMaker.noiseDistance + hearingRadius;
            if (Vector3.Distance(target.transform.position, pawn.transform.position) <=
                sumOfRadii)
            {
                // then can hear
                return true;
            }
            //else
            //can't hear (false)
            return false;
        }
    }

    public virtual void DoTargetNearestPlayerState()
    {
        AITarget = GetNearestPlayer();
    }

    public virtual GameObject GetNearestPlayer()
    {
        //assume player 0 is closest
        GameObject nearestPlayer = GameManager.instance.players[0].pawn.gameObject;
        float nearestPlayerDistance = Vector3.Distance(pawn.transform.position,
                                      GameManager.instance.players[0].pawn.transform.position);

        //check other players to see if closer
        for (int i = 1; i < GameManager.instance.players.Count; i++)
        {
            float tempDistance = Vector3.Distance(pawn.transform.position,
                                  GameManager.instance.players[i].pawn.transform.position);
            if (tempDistance < nearestPlayerDistance)
            {
                nearestPlayer = GameManager.instance.players[i].pawn.gameObject;
                nearestPlayerDistance = tempDistance;
            }
        }

        //every player checked, nearest found
        return nearestPlayer;
    }
}
