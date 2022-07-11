using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base controller abstract class, always has a pawn and a cameracontroller
public abstract class Controller : MonoBehaviour
{

    public Pawn pawn;
    public CameraController cam;

    // Start is called before the first frame update
    protected abstract void Start();

    // Update is called once per frame
    protected abstract void Update();

    protected virtual void ProcessInputs() { }

    public virtual void MakeDecisions() { }
}
