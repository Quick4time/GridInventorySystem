using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson; // Подключаем
using System.IO; // Подключаем

public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData; // Для сохранения качаем LitJson.dll

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); // Создаем папку и создаем тексовый документ с именем Items.json
        ConstructItemDatabase(); // Запускаем метод добавляющий предмет.
    }

    //Извлечь элемент по идентификатору
    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    // Создаем предметы с помощью Словаря коючь/значение в файле Items.json
    void ConstructItemDatabase()
    {
        // Перебераем все предметы в ItemData
        for (int i = 0; i < itemData.Count; i++)
        {
            // добавляем в лист экземпляр класса с параметрами из файла Json.
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["power"], (int)itemData[i]["stats"]["defence"], (int)itemData[i]["stats"]["vitality"], itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()));
        }
    }
}

public class Item
{
    // Добавляем переменные в класс Item с публичным полем, переменные которые нужно будет сохранить и записанные в Items.jason
    public int ID { get; set;}
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; } // Создаем папку Resources

    public Item (int id, string title, int value, int power, int defence, int vitality, string description, bool stackable, int rarity, string slug)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprite/Icons/" + slug); // прикрепляем к нашим обьектам иконки с именем // Доделать Завтра
    }
    // Этот прием делаеться для того что бы если создается класс без параметров то он сразу выгружался так как Id юудет равняться -1.
    public Item()
    {
        this.ID = -1;
    }
}
