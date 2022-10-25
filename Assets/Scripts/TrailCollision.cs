using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollision : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject trailFollower;
    public GameObject colliderPrefab;

    public int poolSize = 5;
    GameObject[] pool;

    // Start is called before the first frame update
    void Start()
    {
        //trail = GetComponent<TrailRenderer>();
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(colliderPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!trail.isVisible)
        {
            for(int i = 0; i < pool.Length; i++)
            {
                pool[i].gameObject.SetActive(false);
            }
        }
        else
        {
            TrailCollisions();
        }
    }

    void TrailCollisions()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].gameObject.activeSelf == false)
            {
                pool[i].gameObject.SetActive(true);
                pool[i].gameObject.transform.position = trailFollower.gameObject.transform.position;
                
                StartCoroutine(hide(5, pool[i].gameObject));
                return;
            }
        }
    }

    private IEnumerator hide(float waitTime, GameObject p)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            p.SetActive(false);
            yield break;
        }
        yield break;
    }
}
