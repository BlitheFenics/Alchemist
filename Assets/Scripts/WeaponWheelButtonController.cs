using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    public string itemName;
    public TextMeshProUGUI itemText, ammo, selectedAmmo;
    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;
    public InventoryManager inventory;

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }

        if (selectedItem.sprite.name == "shiny_rpg_potions_16x16_1")
        {
            selectedAmmo.SetText(inventory.ammo.ToString());
        }
        if (selectedItem.sprite.name == "shiny_rpg_potions_16x16_31")
        {
            selectedAmmo.SetText(inventory.acidAmmo.ToString());
        }
        if (selectedItem.sprite.name == "shiny_rpg_potions_16x16_10")
        {
            selectedAmmo.SetText(inventory.healingAmmo.ToString());
        }
        if (selectedItem.sprite.name == "shiny_rpg_potions_16x16_20")
        {
            selectedAmmo.SetText(inventory.speedAmmo.ToString());
        }

        if (ID == 1)
        {
            ammo.SetText(inventory.ammo.ToString());
        }
        else if(ID == 2)
        {
            ammo.SetText(inventory.acidAmmo.ToString());
        }
        else if (ID == 3)
        {
            ammo.SetText(inventory.healingAmmo.ToString());
        }
        else if (ID == 4)
        {
            ammo.SetText(inventory.speedAmmo.ToString());
        }
    }

    public void Selected()
    {
        selected = true;
        WeaponWheelController.weaponID = ID;
    }

    public void Deselected()
    {
        selected = false;
        //WeaponWheelController.weaponID = 0;
    }

    public void HoverEnter()
    {
        itemText.text = itemName;
    }

    public void HoverExit()
    {
        itemText.text = "";
    }
}
