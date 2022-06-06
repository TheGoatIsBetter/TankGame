using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    //rigidbody
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        //load components to vars
        rigidbodyComponent = this.gameObject.GetComponent<Rigidbody>();
    }

    public override void MoveForward(float speed)
    {
        //move position of rigidbody rank forward at speed
        rigidbodyComponent.MovePosition(transform.position += (transform.forward * (speed * Time.deltaTime)));
    }

    public override void Turn(float speed)
    {
        //Turn tank
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
