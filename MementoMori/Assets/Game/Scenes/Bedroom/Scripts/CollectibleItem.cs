using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData; // Dados do item coletável

    private void Start()
    {
        // Verifica se o itemData está configurado
        if (itemData == null)
        {
            Debug.LogWarning("ItemData não atribuído no item " + gameObject.name);
        }
    }
}
