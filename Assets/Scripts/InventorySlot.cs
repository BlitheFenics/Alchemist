using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    ItemData itemToDisplay;

    public Image itemDisplay;

    int slotIndex;

    public void Display(ItemData itemToDisplay)
    {
        if(itemToDisplay != null)
        {
            itemDisplay.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;
            itemDisplay.gameObject.SetActive(true);
            return;
        }

        itemDisplay.gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.InventoryToHand(slotIndex);
    }

    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }
}
