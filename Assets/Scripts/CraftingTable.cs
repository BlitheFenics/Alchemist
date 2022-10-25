using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject selection, CraftingUI;

    // Start is called before the first frame update
    void Start()
    {
        Select(false);
    }

    public void Select(bool toggle)
    {
        selection.SetActive(toggle);
    }

    public void Interact()
    {
        CraftingUI.SetActive(true);
    }
}
