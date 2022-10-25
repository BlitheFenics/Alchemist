using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Projectile projectile;
    public Transform shootPoint;
    public float projectileSpeed, period = 0.1f;

    private float nextActionTime =  0.0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            Projectile newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as Projectile;
            newProjectile.moveSpeed = projectileSpeed;
        }
    }
}