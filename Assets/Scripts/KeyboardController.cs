using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
    [SerializeField] private KeyCode moveForward;
    [SerializeField] private KeyCode moveBackward;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode shoot;



    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        ProcessInputs();
    }

    protected override void ProcessInputs()
    {
        if (Input.GetKey(moveForward))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackward))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(turnRight))
        {
            pawn.TurnRight();
        }

        if (Input.GetKey(turnLeft))
        {
            pawn.TurnLeft();
        }

        if (Input.GetKey(shoot))
        {
            pawn.Shoot();
        }
    }
}
