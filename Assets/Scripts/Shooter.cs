using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //time before bullet despawns on it's own
    [SerializeField] private float secondsBeforeDespawn;
    public float timeBetweenShots;

    //shoots
    public void Shoot(GameObject bulletPrefab, float shootForce,
                        float damageDone, Pawn shooter, Transform shootPoint)
    {
        //create bullet
        GameObject bullet = Instantiate(bulletPrefab, 
                                        shootPoint.position,  
                                        transform.rotation);

        //send it data from projectile component
        Projectile projectile = bullet.GetComponent<Projectile>();

        //make sure projectile exists
        if(projectile != null)
        {
            //then give it properties
            projectile.damageDone = damageDone;
            projectile.owner = shooter;
        }

        //push it at force
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(transform.forward * shootForce);
        }

        //destroy bullet after 5 seconds
        Destroy(bullet, secondsBeforeDespawn);
    }
}
