using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    private Pawn pawn;
    [SerializeField] private Vector3 camPoint; //point at which camera goes to watch pawn when button held

    // Start is called before the first frame update
    void Start()
    {
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        //if there exists a player
        if(GameManager.instance.players[playerNumber].pawn != null )
        {
            //set it to a simple variable for brevity's sake
            pawn = GameManager.instance.players[playerNumber].pawn;
        }
    }

    //two similar functions, that COULD be one, but are named differently for descriptions sake
    //not sure if better way to do this in C#

    //follows the pawn simply
    public void followPawn()
    {
        if (pawn != null)
        {
            //set position of camera to the offset of the position
            transform.SetPositionAndRotation(pawn.transform.position + pawn.cameraOffset, Quaternion.identity);

            //look at the pawn
            transform.LookAt(pawn.transform);
        }
    }

    //watches pawn from a point
    public void watchPawnFromCenter()
    {
        if (pawn != null)
        {
            //set position of camera to point
            transform.SetPositionAndRotation(camPoint, Quaternion.identity);

            //look at pawn
            transform.LookAt(pawn.transform);
        }
    }
}
