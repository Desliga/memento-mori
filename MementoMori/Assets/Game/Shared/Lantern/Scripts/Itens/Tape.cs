using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour, ICollect
{
    public SO_ItemData ItemData;

    public void Collect()
    {
        GameManager.Instance.AddItemToInventory(ItemData);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}