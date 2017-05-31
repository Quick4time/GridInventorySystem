using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventory : MonoBehaviour
{
    Inventory inv;
    ItemDatabase database;
    GameObject inventoryPanel;
    GameObject equipedPanel;
    [SerializeField]
    GameObject inventorySlot;

    int slotsNum;
    
    public List<Item> items = new List<Item>();
    [HideInInspector]
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        database = GetComponent<ItemDatabase>();
        inv = GetComponent<Inventory>();
        slotsNum = 5;
        inventoryPanel = GameObject.Find("Inventory Panel");
        equipedPanel = inventoryPanel.transform.FindChild("EquipPanel").gameObject;

        for (int i = 0; i < slotsNum; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<SlotInventory>().id = i;
            slots[i].transform.SetParent(equipedPanel.transform);
        }
        inv.AddItem(0);
    }
}
