using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private GameObject craftingMenu;
    public void Interact()
    {
        craftingMenu.SetActive(true);
    }
}
