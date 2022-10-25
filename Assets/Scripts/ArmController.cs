using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public bool isThrowing;

    public PotionController potion, acid;
    public float potionSpeed;
    //public int ID;

    public float timeBetweenThrows;
    private float throwCounter;

    public Transform throwPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int ID = WeaponWheelController.weaponID;

        if (isThrowing)
        {
            throwCounter -= Time.deltaTime;
            if(throwCounter <= 0)
            {
                
                //throwCounter = timeBetweenThrows;
                throwCounter = 10;
                if (ID == 1)
                {
                    PotionController newPotion = Instantiate(potion, throwPoint.position, throwPoint.rotation) as PotionController;
                    newPotion.moveSpeed = potionSpeed;
                }
                if (ID == 2)
                {
                    PotionController newAcid = Instantiate(acid, throwPoint.position, throwPoint.rotation);
                    newAcid.moveSpeed = potionSpeed;
                }
            }
        }
        else
        {
            throwCounter = 0;
        }
    }
}
