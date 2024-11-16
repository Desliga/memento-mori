using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemData itemData; // Dados do item colet�vel

    private void Start()
    {
        // Verifica se o itemData est� configurado
        if (itemData == null)
        {
            Debug.LogWarning("ItemData n�o atribu�do no item " + gameObject.name);
        }
    }
}
