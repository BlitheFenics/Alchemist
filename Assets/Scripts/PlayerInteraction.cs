using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController playerController;
    FarmLand selectedFarmLand = null;
    InteractableObject selectedInteractable = null;
    CraftingTable selectedCraftingTable = null;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        
        if(other.tag == "FarmLand")
        {
            FarmLand farmLand = other.GetComponent<FarmLand>();
            SelectFarmLand(farmLand);
            return;
        }

        if(other.tag == "Item")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            return;
        }

        if(other.tag == "CraftingTable")
        {
            CraftingTable craftingTable = other.GetComponent<CraftingTable>();
            SelectCraftingTable(craftingTable);
            return;
        }

        if(selectedInteractable != null)
        {
            selectedInteractable = null;
        }

        if(selectedFarmLand != null)
        {
            selectedFarmLand.Select(false);
            selectedFarmLand = null;
        }

        if(selectedCraftingTable != null)
        {
            selectedCraftingTable.Select(false);
            selectedCraftingTable = null;
        }
    }

    void SelectFarmLand(FarmLand farmland)
    {
        if(selectedFarmLand != null)
        {
            selectedFarmLand.Select(false);
        }

        selectedFarmLand = farmland;
        farmland.Select(true);
    }

    void SelectCraftingTable(CraftingTable craftingTable)
    {
        if (selectedCraftingTable != null)
        {
            selectedCraftingTable.Select(false);
        }

        selectedCraftingTable = craftingTable;
        craftingTable.Select(true);
    }

    public void Interact()
    {
        if(selectedFarmLand != null)
        {
            selectedFarmLand.Interact();

            return;
        }

        //Debug.Log("Not on land");
    }

    public void ItemInteract()
    {
        if(InventoryManager.Instance.equippedItem != null)
        {
            InventoryManager.Instance.HandToInventory();
            return;
        }

        if(selectedInteractable != null)
        {
            selectedInteractable.Pickup();
        }
    }

    public void CraftingTableInteract()
    {
        if (selectedCraftingTable != null)
        {
            selectedCraftingTable.Interact();

            return;
        }
    }
}
