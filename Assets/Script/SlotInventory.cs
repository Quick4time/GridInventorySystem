using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInventory : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();// Поменять способ доступа для более быстрой инициализации.
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>(); // Получаем всю информацию о предмете от ItemData.
        Debug.Log(inv.items[id].ID);
        if (inv.items[id].ID == -1) // Означает что слот пустой. 
        {
            inv.items[droppedItem.slot] = new Item(); // в слоте создаем новый предмет.
            inv.items[id] = droppedItem.item;
            droppedItem.slot = id; // указываем слоту в который помещен droppedItem id.
        }
        else // Если слот занят другим предметом. Меняем их местами // Будут проблемы else if (droppedItem.slot != id)
        {
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);
            item.transform.position = inv.slots[droppedItem.slot].transform.position;

            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.items[id] = droppedItem.item;
            droppedItem.slot = id;
        }
    }
}
/*
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);
            item.transform.position = inv.slots[droppedItem.slot].transform.position;

            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.items[id] = droppedItem.it
*/
