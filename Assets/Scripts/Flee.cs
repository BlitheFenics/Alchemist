using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    public float enemyDistanceRun = 4.0f;
    int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distance: " + distance);

        if(distance < enemyDistanceRun)
        {
            state = 1;
            if (player.GetComponent<PlayerController>().tracks != 3)
            {
                player.GetComponent<PlayerController>().BGM(3);
            }

            Vector3 dirToPlsyer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlsyer;
            agent.SetDestination(newPos);

        }

        if (distance > enemyDistanceRun && state == 1)
        {
            state = 2;
            if (player.GetComponent<PlayerController>().tracks != 2)
            {
                player.GetComponent<PlayerController>().BGM(2);
            }
        }
    }
}
