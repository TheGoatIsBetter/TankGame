using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
    //editable keycodes
    [SerializeField] private KeyCode moveForward;
    [SerializeField] private KeyCode moveBackward;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode shoot;
    [SerializeField] private KeyCode pointCam;



    // Start is called before the first frame update
    protected override void Start()
    {
        //add itself to list of players
        GameManager.instance.players.Add(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        //make sure pawn exists
        if(pawn != null)
        {
            ProcessInputs();
        }
    }

    public void OnDestroy()
    {
        //remove itself from list of players
        GameManager.instance.players.Remove(this);
    }

    //simply checks for keycodes and runs functions from pawn or cameracontroller if down/pressed
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

        if (Input.GetKeyDown(shoot))
        {
            pawn.Shoot();
        }

        if (Input.GetKey(pointCam))
        {
            cam.watchPawnFromCenter();
        }
        else
        {
            cam.followPawn();
        }
    }
}
