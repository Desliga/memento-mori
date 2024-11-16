using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public LayerMask itemLayer;       // Layer dos itens a serem coletados
    public Light lanternLight;        // Luz da lanterna
    public float focusTime = 3f;      // Tempo necessário para coletar o item
    private float focusProgress = 0f; // Progresso do foco

    private GameObject focusedItem;
    private bool isFocusing;

    private void Update()
    {
        Ray ray = new Ray(lanternLight.transform.position, lanternLight.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, lanternLight.range, itemLayer))
        {
            // Detecta o item na layer correta
            if (hit.collider.gameObject != focusedItem)
            {
                ResetFocus();
                focusedItem = hit.collider.gameObject;
                isFocusing = true;
            }

            // Aumenta o foco da luz e coleta o item após um tempo
            if (isFocusing)
            {
                focusProgress += Time.deltaTime;
                lanternLight.spotAngle = Mathf.Lerp(80, 30, focusProgress / focusTime);

                if (focusProgress >= focusTime)
                {
                    CollectItem(focusedItem);
                    ResetFocus();
                }
            }
        }
        else
        {
            ResetFocus();
        }
    }

    private void ResetFocus()
    {
        isFocusing = false;
        focusProgress = 0f;
        focusedItem = null;
        lanternLight.spotAngle = 80; // Ângulo padrão da luz
    }

    private void CollectItem(GameObject item)
    {
        Inventory.Instance.AddItem(item.GetComponent<CollectibleItem>().itemData);
        Destroy(item); // Remove o item da cena
    }
}
