using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 2.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
