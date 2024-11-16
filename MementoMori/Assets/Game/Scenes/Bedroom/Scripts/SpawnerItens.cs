using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItens : MonoBehaviour
{
    
    [Header("Configurações do Spawn")]
    public List<GameObject> itemPrefabs; 
    public List<Transform> spawnPoints; 

    private List<GameObject> spawnedItems = new List<GameObject>(); 

    void Start()
    {
        
        SpawnItems();
    }

    // Função para instanciar os itens conforme a ordem definida
    public void SpawnItems()
    {
  
        int spawnnumber = Random.Range(0, spawnPoints.Count);
        GameObject spawnedItem = Instantiate(itemPrefabs[0], spawnPoints[spawnnumber].position, spawnPoints[spawnnumber].rotation);
        spawnedItems.Add(spawnedItem); 
    }

    
    public void RespawnItems()
    {
        
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item); // Destroi o item existente
        }
        spawnedItems.Clear(); // Limpa a lista de itens instanciados

        // Reinstancia os itens novamente
        SpawnItems();
    }
}
