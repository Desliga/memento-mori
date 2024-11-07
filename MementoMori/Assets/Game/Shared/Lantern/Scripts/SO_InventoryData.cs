using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_InventoryData", menuName = "ScriptableObjects/SO_InventoryData")]
public class SO_InventoryData : ScriptableObject, ISerializationCallbackReceiver
{
    public List<SO_ItemData> itens;

    public void AddItem(SO_ItemData item)
    {
        itens.Add(item);
    }

    public void OnAfterDeserialize()
    {
        itens.Clear();
    }

    public void OnBeforeSerialize()
    {
        
    }
}
