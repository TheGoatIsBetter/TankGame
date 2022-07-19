using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [SerializeField] public float turnSpeed;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float baseMoveSpeed;
    [SerializeField] public float maxMoveSpeed;
    protected Mover mover;
    public Vector3 cameraOffset;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //load mover
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //abstract function declarations
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnRight();
    public abstract void TurnLeft();
    public abstract void Shoot();
    public abstract void TurnTowards(Vector3 targetPos);
}
