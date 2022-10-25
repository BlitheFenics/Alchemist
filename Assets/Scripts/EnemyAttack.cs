using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
    }
}
