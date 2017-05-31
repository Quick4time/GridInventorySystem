using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
// Ui.Image
// Добавить Content Size Fitter.VerticalFit.PreferredSize
// Добавить Horizontal Layout Group Padding(Left,Right,Top,Bottom)"7"all
// Add Canvas Group = iteractable.false && Blocks Raycasts.false
public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item) // Активация Tooltip
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate() // Деактивация Tooltip
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() // Формируем текс передаваемый tooltip
    {
        data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\n" + item.Description + "\nPower" + item.Power;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data; // получаем го далее получаем текс и передаем дату.
    }
}
