using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private List<ItemData> items = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(ItemData itemData)
    {
        items.Add(itemData);
        Debug.Log("Item coletado: " + itemData.itemName);
    }

    public List<ItemData> GetItems()
    {
        return items;
    }
}
