using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIStates { Idle, Chase, Attack, Flee, ChooseTarget, Patrol, CoverFire, Sentry};
    [SerializeField] protected AIStates currentState;
    protected float timeEnteredCurrentState;
    [SerializeField] protected GameObject AITarget;
    [SerializeField] protected List<Transform> waypoints;
    protected int currentWaypoint;
    [SerializeField] protected float hearingRadius;
    [SerializeField] protected float fieldOfView;
    [SerializeField] protected float shootFieldOfView;
    [SerializeField] protected float viewDistance;
    [SerializeField] protected GameObject dummyTarget;


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
        //set movespeed to max
        pawn.moveSpeed = pawn.maxMoveSpeed;

        //chase the player
        Chase(AITarget);
    }

    public virtual void DoChooseTargetState()
    {
        AITarget = GetNearestPlayer();
    }

    public virtual void DoAttackState()
    {
        pawn.moveSpeed = pawn.baseMoveSpeed;
        //Chase the player
        Chase(AITarget);

        //shoot at them
        if (CanSee(AITarget, shootFieldOfView))
        {
            pawn.Shoot();
        }
        
    }

    public virtual void DoPatrolState()
    {
        pawn.moveSpeed = pawn.baseMoveSpeed;

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

    public virtual void DoFleeState()
    {
        Flee(AITarget.transform.position, false);
    }

    public virtual void DoCoverFireState()
    {
        Flee(AITarget.transform.position, true);
        pawn.Shoot();
    }

    public virtual void DoSentryState()
    {
        pawn.TurnTowards(AITarget.transform.position);
        pawn.Shoot();
    }


    public virtual void Flee(Vector3 fleeTarget, bool backwards)
    {
        //get distance
        float dist = Vector3.Distance(fleeTarget, pawn.transform.position);
        //set speed based off distance
        float maxFleeDist = 10.0f;
        float fleeDistPercent = dist / maxFleeDist;
        float fleeSpeed = (1 - fleeDistPercent) * maxFleeDist;

        if (fleeSpeed > pawn.maxMoveSpeed) fleeSpeed = pawn.maxMoveSpeed;

        pawn.moveSpeed = fleeSpeed;

        if (backwards)
        {
            pawn.TurnTowards(fleeTarget);
            pawn.MoveBackward();
        }
        else
        {
            //turn away from player
            Vector3 distance = fleeTarget - pawn.transform.position;
            Vector3 endPoint = pawn.transform.position - new Vector3(distance.x, pawn.transform.position.y, distance.z);
            pawn.TurnTowards(endPoint);

            //move forward
            pawn.MoveForward();
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

    public virtual bool CanSee( GameObject target, float FoV)
    {
        if (target == null) return false; //prevent missingreference

        //check if in FoV
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        float angleToTarget = Vector3.Angle(pawn.transform.forward, vectorToTarget);
        if(angleToTarget > FoV)
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
        if (target == null) return false; //prevent nullreference

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
        if (GameManager.instance.players[0].pawn == null) return null;

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

        Debug.Log(nearestPlayer);
        //every player checked, nearest found
        return nearestPlayer;
    }

    //returns whether nearest AI to nearest player or not
    public virtual bool IsNearestAI()
    {
        //nearest player
        GameObject nearestPlayer = GetNearestPlayer();

        //check other ais to see if closer
        for (int i = 1; i < GameManager.instance.ais.Count; i++)
        {
            float tempDistance = Vector3.Distance(nearestPlayer.transform.position,
                                  GameManager.instance.ais[i].pawn.transform.position);
            if (tempDistance < Vector3.Distance(pawn.transform.position, nearestPlayer.transform.position))
            {
                return false;
            }
        }

        return true;
    }
}
