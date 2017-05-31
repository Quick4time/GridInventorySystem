using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// Важно !!!!!! Inventory MonoManager ScriptExecutionOrder (ItemDatabase then Inventory)
public class Inventory : MonoBehaviour
{
    ItemDatabase database;
    GameObject inventoryPanel;
    GameObject slotPanel;
    [SerializeField]
    GameObject inventorySlot;
    [SerializeField]
    GameObject inventoryItem;
    //GameObject equipPanel;

    int slotAmount; // Количество слотов инвентаря.
    public List<Item> items = new List<Item>();
    [HideInInspector]
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        database = GetComponent<ItemDatabase>();
        // Переделать ссылки для более быстрой инициализации т.к Find слишком медленный.// либо SerializeField.
        slotAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        //equipPanel = inventoryPanel.transform.FindChild("Panel").gameObject;
        for (int i = 0; i < slotAmount; i++) // Используем цикл для создания слотов в соответствии со slotAmount
        {
            items.Add(new Item()); // item == null
            slots.Add(Instantiate(inventorySlot)); // создаем prefab слота.
            slots[i].GetComponent<SlotInventory>().id = i;
            slots[i].transform.SetParent(slotPanel.transform); // затем помещаем его в Parent slotPanel
        }
        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(1);
        AddItem(1);

        /*
        for (int i = 0; i < 1; i++)
        {
            items.Add(new Item()); // item == null
            slots.Add(Instantiate(inventorySlot)); // создаем prefab слота.
            slots[i].GetComponent<SlotInventory>().id = i;
            slots[i].transform.SetParent(equipPanel.transform); // затем помещаем его в Parent slotPanel
        }
        */
        for (int i = 0; i < items.Count; i++) // перебераем вещи в инвентаре и узнаем общее количество параметров предмета (может сделать метод?).
        {
            if(items[i].ID != -1)
            {
                if (items[i].ID )
                //ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                print((items[i]).ToString()); 
            }
        }
        //print(AmountItemsValue(items, slots, 0));
    }

    int AmountItemsValue (List<Item> item, List<GameObject> slots, int itemID)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if(item[i].ID == itemID)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                int amountItemParam = item[i].Value * data.amount;
                return amountItemParam;
            }
        }
        return 0;
    }
    

    // Метод добавления предмета в инвентарь по id
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id); // используем метод с публичным полем из скрипта ItemDatabase 
        if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd)) // если параметр stackable true && если этот предмет находиться в инвентаре.
        {
            for(int i = 0; i < items.Count; i++ ) // перебераем все предметы  в List<items>;
            {
                if (items[i].ID == id) // сравниваем предметы по id  и если они совпадают.
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>(); // получаем 
                    data.amount++;  // добавляем одно значение к data.amount
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++) // далее перебераем ячейки в слоте.
            {
                if (items[i].ID == -1) // В тех ячейках которых items[i].ID == -1 т.е null
                {
                    items[i] = itemToAdd; // стачала извлекаем его из базы данных
                    GameObject itemObj = Instantiate(inventoryItem); // затем создаем игровой обьект предмета
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i; // Указываем текущий слот для предмета.
                    itemObj.transform.SetParent(slots[i].transform); // Перемещаем в Слот
                    itemObj.transform.position = slots[i].transform.position; // и устанавливаем внутри Parent 0,0 по осям x, y что означает по центру.
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite; // Делаем иконку предмета. 
                    itemObj.name = itemToAdd.Title; // Устанавливаем имя предмету в инспекторе
                    break; // Если нашли слот останавливаем итерацию
                }
            }
        }
    }

    bool CheckIfItemIsInInventory(Item item) // Проверяем есть ли этот предмет в инвентаре, для stackableItem
    {
        for(int i = 0; i < items.Count; i++) // перебираес предметы в инвентаре
        {
            if (items[i].ID == item.ID) // если находим с одинаковым id
            {
                return true; // возвращаем true
            }
        }
        return false; // если нет то возвращаем false
    }
}
