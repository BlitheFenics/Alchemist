using UnityEngine;

using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("WeaponWheel"))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if(weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 0:
                selectedItem.sprite = noImage;
                break;
            case 1:
                //Debug.Log("Potion Battle");
                break;
            case 2:
                //Debug.Log("Acid Bottle");
                break;
        }
    }
}
