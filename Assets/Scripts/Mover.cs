using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract void MoveForward(float speed);
    public abstract void Turn(float speed);
}
