using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : MonoBehaviour, TimeTracker
{
    public GameObject selection;

    GameTime time;

    [Header("Crops")]
    public GameObject cropPrefab;
    CropBehaviour cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        Select(false);

        TimeManager.Instance.RegisterTracker(this);
    }

    public void Select(bool toggle)
    {
        selection.SetActive(toggle);
    }

    public void Interact()
    {
        ItemData toolSlot = InventoryManager.Instance.equippedItem;

        if(toolSlot == null)
        {
            return;
        }

        SeedData seedTool = toolSlot as SeedData;

        if(seedTool != null && cropPlanted == null)
        {
            GameObject cropObject = Instantiate(cropPrefab, transform);

            cropObject.transform.position = new Vector3(transform.position.x, 0.56f, transform.position.z);

            cropPlanted = cropObject.GetComponent<CropBehaviour>();

            cropPlanted.Plant(seedTool);

            if (InventoryManager.Instance.equippedItem.name == "Tomato Seed" || InventoryManager.Instance.equippedItem.name == "Corn Seed" || InventoryManager.Instance.equippedItem.name == "Pumpkin Seed"  || InventoryManager.Instance.equippedItem.name == "Eggplant Seed")
            {
                InventoryManager.Instance.equippedItem = null;
            }

            time = TimeManager.Instance.GetGameTime();
        }
    }

    public void ClockUpdate(GameTime timeStamp)
    {
        //int hoursElapsed = GameTime.CompareTimeStamps(time, timeStamp);

        if(cropPlanted != null)
        {
            int hoursElapsed = GameTime.CompareTimeStamps(time, timeStamp);
            cropPlanted.Grow();
        }
    }
}
