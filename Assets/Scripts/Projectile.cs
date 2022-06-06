using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //how much damage bullet does
    public float damageDone;
    //who shot it?
    public Pawn owner;

    //when this enters another object, run
    public void OnTriggerEnter(Collider other)
    {
        //gets hit object's health
        Health otherHealth = other.gameObject.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damageDone, owner);
        }

        //kill self
        Destroy(this.gameObject);
    }
}
