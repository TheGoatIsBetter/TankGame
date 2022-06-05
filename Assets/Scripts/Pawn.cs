using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [SerializeField] protected float turnSpeed;
    [SerializeField] protected float moveSpeed;
    protected Mover mover;

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

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnRight();
    public abstract void TurnLeft();
    public abstract void Shoot();
}
