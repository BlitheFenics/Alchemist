using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public ArmController arm;
    public ItemData tomatoSeed, cornSeed, pumpkinSeed, eggplantSeed;
    public int ammo = 0, acidAmmo = 0, healingAmmo = 0, speedAmmo = 0, ID;
    public float period = 10f;
    private float nextActionTime = 0.0f;
    public PlayerController player;
    public NavMeshAgent shieldEnemy;

    public AudioClip fired, smash;
    public AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }

    [Header("Items")]
    public ItemData[] inventory = new ItemData[24];
    public ItemData equippedItem = null;
    public Transform handPoint;

    public void InventoryToHand(int slotIndex)
    {
        ItemData itemToEquip = inventory[slotIndex];

        inventory[slotIndex] = equippedItem;

        equippedItem = itemToEquip;

        RenderHand();

        UIManager.Instance.RenderInventory();
    }

    public void HandToInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = equippedItem;
                equippedItem = null;
                break;
            }
        }

        RenderHand();

        UIManager.Instance.RenderInventory();
    }

    public void RenderHand()
    {
        if(handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }

        if(equippedItem != null)
        {
            Instantiate(equippedItem.gameModel, handPoint);
        }
    }

    public void CraftTomatoSeed()
    {
        bool craft = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Tomato")
            {
                inventory[i] = tomatoSeed;
                craft = true;
                break;
            }
        }
        if(craft == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = tomatoSeed;
                    craft = false;
                    return;
                }
            }
        }
        return;
    }

    public void CraftCornSeed()
    {
        bool craft = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Corn")
            {
                inventory[i] = cornSeed;
                craft = true;
                break;
            }
        }
        if (craft == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = cornSeed;
                    craft = false;
                    return;
                }
            }
        }
        return;
    }

    public void CraftPumpkinSeed()
    {
        bool craft = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Pumpkin")
            {
                inventory[i] = pumpkinSeed;
                craft = true;
                break;
            }
        }
        if (craft == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = pumpkinSeed;
                    craft = false;
                    return;
                }
            }
        }
        return;
    }

    public void CraftEggplantSeed()
    {
        bool craft = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Eggplant")
            {
                inventory[i] = eggplantSeed;
                craft = true;
                break;
            }
        }
        if (craft == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = eggplantSeed;
                    craft = false;
                    return;
                }
            }
        }
        return;
    }

    public void CraftDamageBottle()
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                continue;
            }
            if(inventory[i].name == "Tomato")
            {
                inventory[i] = null;
                ammo += 10;
                break;
            }
        }
        return;
    }

    public void CraftAcidBottle()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Corn")
            {
                inventory[i] = null;
                acidAmmo += 10;
                break;
            }
        }
        return;
    }

    public void CraftHealingPotion()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Pumpkin")
            {
                inventory[i] = null;
                healingAmmo += 10;
                break;
            }
        }
        return;
    }

    public void CraftSpeedPotion()
    {
        bool craft = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                continue;
            }
            if (inventory[i].name == "Eggplant")
            {
                inventory[i] = null;
                craft = true;
                break;
            }
        }
        if (craft == true)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    continue;
                }
                if (i >= inventory.Length && inventory[i].name != "Corn")
                {
                    for (int y = 0; y < inventory.Length; y++)
                    {
                        if (inventory[y] == null)
                        {
                            inventory[y] = eggplantSeed;
                            craft = false;
                            break;
                        }
                    }
                    break;
                }
                if (inventory[i].name == "Corn")
                {
                    inventory[i] = null;
                    speedAmmo += 10;
                    craft = false;
                    return;
                }
            }
        }
        return;
    }

    void Update()
    {
        float shoot = Input.GetAxis("Shoot");
        ID = WeaponWheelController.weaponID;

        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            player.moveSpeed = 3;
            shieldEnemy.angularSpeed = 9999;
        }

        if (Input.GetButtonDown("Shoot") && ammo > 0 && ID == 1)// || shoot > 0 && ammo > 0 && ID == 1)
        {
            arm.isThrowing = true;

            source.PlayOneShot(fired);

            if(ammo > 0 && ID == 1)
            {
                ammo--;
            }
        }
        else if(ammo <= 0 && ID == 1)
        {
            arm.isThrowing = false;
        }

        if (Input.GetButtonDown("Shoot") && acidAmmo > 0 && ID == 2)// || shoot > 0 && acidAmmo > 0 && ID == 2)
        {
            arm.isThrowing = true;

            source.PlayOneShot(fired);

            if (acidAmmo > 0 && ID == 2)
            {
                acidAmmo--;
            }
        }
        else if (acidAmmo <= 0 && ID == 2)
        {
            arm.isThrowing = false;
        }

        if (Input.GetButtonDown("Shoot") && healingAmmo > 0 && ID == 3)// || shoot > 0 && healingAmmo > 0 && ID == 3)
        {
            arm.isThrowing = true;

            if (healingAmmo > 0 && ID == 3)
            {
                if(player.currentHealth <= 90)
                {
                    player.currentHealth += 10;
                    healingAmmo--;
                }
                else if(player.currentHealth >= 90 && player.currentHealth < 100)
                {
                    player.currentHealth = 100;
                    healingAmmo--;
                }
            }
        }
        else if (healingAmmo <= 0 && ID == 3 || player.currentHealth >= 100 && ID == 3)
        {
            arm.isThrowing = false;
        }

        if (Input.GetButtonDown("Shoot") && speedAmmo > 0 && ID == 4)// || shoot > 0 && speedAmmo > 0 && ID == 4)
        {
            arm.isThrowing = true;

            if (speedAmmo > 0 && ID == 4)
            {
                if (player.moveSpeed == 3)
                {
                    player.moveSpeed = 4.5f;
                    shieldEnemy.angularSpeed = 50;
                    speedAmmo--;
                }
            }
        }
        else if (speedAmmo <= 0 && ID == 4 || player.moveSpeed == 4.5f && ID == 4)
        {
            arm.isThrowing = false;
        }

        if (Input.GetButtonUp("Shoot") )//|| shoot <= 0)
        {
            arm.isThrowing = false;
        }
    }
    /*
    public bool ContainsItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                return true;
            }
        }
        return false;
    }

    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool isFull()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public int ItemCount(ItemData item)
    {
        int number = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                number++;
            }
        }
        return number;
    }*/
}
