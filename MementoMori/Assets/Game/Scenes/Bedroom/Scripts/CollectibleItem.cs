using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    
    public ItemData itemData; // Dados do item colet�vel

    private void Start()
    {
        // Verifica se o itemData est� configurado
        if (itemData == null)
        {
            Debug.LogWarning("ItemData n�o atribu�do no item " + gameObject.name);
        }
    }

    public void SetRange(float range)
    {
        meshRenderer.materials[1].SetFloat("_Range", range * 0.8f + 0.6f);
    }
}
