using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private Shooter shooter;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float damageDone;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    private float countdown;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        //call parent start
        base.Start();

        //get shooter component of tankpawn
        shooter = GetComponent<Shooter>();

        //set countdown to time between shots
        countdown = shooter.timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        //decrease countdown by time passed
        countdown -= Time.deltaTime;
    }

    public override void MoveForward()
    {
        //use the mover to move forward if not null
        if (mover != null)
        {
            mover.MoveForward(moveSpeed);
        }

    }
    public override void MoveBackward()
    {
        //move backward
        if (mover != null)
        {
            mover.MoveForward(-moveSpeed);
        }
    }
    public override void TurnRight()
    {
        //turn right
        if (mover != null)
        {
            mover.Turn(turnSpeed);
        }
    }
    public override void TurnLeft()
    {
        //left
        if (mover != null)
        {
            mover.Turn(-turnSpeed);
        };
    }
    public override void Shoot()
    {
        //check for countdown over or not (and make sure shooter exists yet)
        if(countdown <= 0 && shooter != null)
        {
            //shoot with shooter
            shooter.Shoot(bulletPrefab, shootForce, damageDone, this, shootPoint);
            //reset countdown
            countdown = shooter.timeBetweenShots;
        }

    }

    public override void TurnTowards(Vector3 targetPos)
    {
        //find vector to target pos from pos
        Vector3 vectorToTargetPos = targetPos - transform.position;
        //find quaternion needed to look down vector
        Quaternion lookRot = Quaternion.LookRotation(vectorToTargetPos, transform.up);
        //change rotation to be slightly down quaternion
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                      lookRot,
                                                      turnSpeed * Time.deltaTime);
    }
}
