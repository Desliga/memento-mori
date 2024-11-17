using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Bedroom.Scripts;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public BedroomStageController bedroomStageController;
    
    public LayerMask interactableLayer;
    public LayerMask itemLayer;       // Layer dos itens a serem coletados
    public Light lanternLight;        // Luz da lanterna
    public float focusTime = 3f;      // Tempo necess�rio para coletar o item
    private float focusProgress = 0f; // Progresso do foco

    private GameObject focusedItem;
    private bool isFocusing;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(lanternLight.transform.position, lanternLight.transform.forward * lanternLight.range);
    }

    private void Update()
    {
        Ray ray = new Ray(lanternLight.transform.position, lanternLight.transform.forward);

        if (Physics.Raycast(ray, out var hit, lanternLight.range, itemLayer))
        {

            Debug.Log("ok");
   
            if (hit.collider.gameObject != focusedItem)
            {
                ResetFocus();
                focusedItem = hit.collider.gameObject;
                isFocusing = true;
            }

            // Aumenta o foco da luz e coleta o item ap�s um tempo
            if (isFocusing)
            {
                focusProgress += Time.deltaTime;
                //lanternLight.spotAngle = Mathf.Lerp(80, 30, focusProgress / focusTime);
                Debug.Log("To aqui");

                hit.collider.gameObject.GetComponent<CollectibleItem>().SetRange(focusProgress/focusTime);
                
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


        if (Physics.Raycast(ray, out var hit2, lanternLight.range, interactableLayer))
        {
            if (hit2.collider.gameObject.TryGetComponent<ILanternInteractable>(out var component))
            {
                component.Interact();
            }
        }
    }

    private void ResetFocus()
    {
        isFocusing = false;
        focusProgress = 0f;
        focusedItem = null;
        lanternLight.spotAngle = 80; // �ngulo padr�o da luz
    }

    private void CollectItem(GameObject item)
    {
        Inventory.Instance.AddItem(item.GetComponent<CollectibleItem>().itemData);
        Destroy(item); // Remove o item da cena
        
        bedroomStageController.NextStage();
    }
}
