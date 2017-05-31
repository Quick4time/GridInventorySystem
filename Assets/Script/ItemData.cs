using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems; // подключаем EventSystem для перетаскивания обьектов.

// Важно в Prefab Item AddComponent Layout Element and Set Ignore Layout
public class ItemData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler // Реализуем интерфейсы для перетаскивания обьектов.1)Начинаем перетаскивать 2)Претаскиваем 3)Завершаем перетаскивать
{
    public Item item;
    public int amount;
    public int slot; // к какому слоту пренадлежит предмет

    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>(); // Кэшируем начальный Parent т.е Ячейку
        tooltip = inv.GetComponent<Tooltip>();
    }

    // Начинаем перетаскивать обьект.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null) // Проверяем наличие предмета в ячейке
        {
            offset = eventData.position - (Vector2)this.transform.position;// или new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent.parent); // при перемещении по осям, перемещаем предмет в Heirarchy в slotPanel.
            this.transform.position = eventData.position - offset; // перемещаем обьект к позиции mousePosition
            GetComponent<CanvasGroup>().blocksRaycasts = false; // Нужно получить CanvasGroup и установить BlockRaycasts = false.
        }
    }

    // Перетаскиваем предмет
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    // Заканчиваем перетаскивать предмет
    public void OnEndDrag(PointerEventData eventData)
    {
        // при окончании перетаскивания возвращаем начальный Parent в Hierarchy
        Debug.Log("Threy");
        this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true; // Нужно получить CanvasGroup и установить BlockRaycasts = true.
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
