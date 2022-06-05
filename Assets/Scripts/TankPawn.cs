using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void MoveForward()
    {
        mover.MoveForward(moveSpeed);
    }
    public override void MoveBackward()
    {
        mover.MoveForward(-moveSpeed);
    }
    public override void TurnRight()
    {
        mover.Turn(turnSpeed);
    }
    public override void TurnLeft()
    {
        mover.Turn(-turnSpeed);
    }
    public override void Shoot()
    {
        Debug.Log("pew");
    }
}
