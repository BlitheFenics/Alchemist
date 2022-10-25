using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().tracks != 2)
        {
            collision.gameObject.GetComponent<PlayerController>().BGM(2);
        }

        else if (collision.gameObject.GetComponent<PlayerController>().tracks != 4)
        {
            collision.gameObject.GetComponent<PlayerController>().BGM(4);
        }
    }
}
