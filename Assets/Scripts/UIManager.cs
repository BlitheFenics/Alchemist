using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, TimeTracker
{
    public static UIManager Instance { get; private set; }

    [Header("Status Bar")]
    public Image equipmentSlot;
    public Text timeText;

    [Header("Inventory System")]
    public GameObject inventoryPanel;
    public InventorySlot[] itemSlots;
    public Text itemNameText;
    public Text itemDescriptionText;
    public HandInventorySlot equipSlot;

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

    private void Start()
    {
        RenderInventory();
        AssignSlotIndex();

        TimeManager.Instance.RegisterTracker(this);
    }

    private void Update()
    {
        ToggleInventoryPanel();
    }

    public void AssignSlotIndex()
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].AssignIndex(i);
        }
    }

    public void RenderInventory()
    {
        ItemData[] inventoryItemSlots = InventoryManager.Instance.inventory;

        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        equipSlot.Display(InventoryManager.Instance.equippedItem);

        ItemData equippedItem = InventoryManager.Instance.equippedItem;

        if (equippedItem != null)
        {
            equipmentSlot.sprite = equippedItem.thumbnail;
            equipmentSlot.gameObject.SetActive(true);
            return;
        }

        equipmentSlot.gameObject.SetActive(false);
    }

    void RenderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }
    
    public void ToggleInventoryPanel()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);

            RenderInventory();
        }
    }

    public void DisplayItemInfo(ItemData data)
    {
        if(data == null)
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            return;
        }

        itemNameText.text = data.name;
        itemDescriptionText.text = data.description;
    }

    public void ClockUpdate(GameTime timestamp)
    {
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        string prefix = " AM";

        if(hours > 12)
        {
            prefix = " PM";
            hours -= 12;
        }

        timeText.text = hours + ":" + minutes.ToString("00") + prefix;
    }
}
