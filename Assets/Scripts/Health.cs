using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    //take damage
    public void TakeDamage(float amount, Pawn owner)
    {
        //take the damage
        currentHealth -= amount;

        //if you have less than 0 health you die
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    //die
    //all it does is destroy the object rn
    public void Die()
    {
        Destroy(this.gameObject);

    }

    
}
