using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public int acid;
    public AudioClip smash;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        Destroy(gameObject, 1);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyHealth>())
        {
            
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
            source.PlayOneShot(smash);
        }
        
        if(collision.gameObject.GetComponent<ShieldHealth>())
        {
            
            collision.gameObject.GetComponent<ShieldHealth>().TakeAcidDamage(acid);
            Destroy(gameObject);
            source.PlayOneShot(smash);
        }
    }
}