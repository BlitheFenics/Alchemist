using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("" + currentHealth);
        txt.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
