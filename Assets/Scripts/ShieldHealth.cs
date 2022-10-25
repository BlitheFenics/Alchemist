using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ShieldHealth : MonoBehaviour
{
    public int maxAcidHealth;
    public int currentAcidHealth;

    public Text txt;

    // Start is called before the first frame update
    void Start()
    {
        currentAcidHealth = maxAcidHealth;
    }

    public void TakeAcidDamage(int acid)
    {
        currentAcidHealth -= acid;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("" + currentAcidHealth);
        txt.text = currentAcidHealth.ToString();
        if (currentAcidHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
