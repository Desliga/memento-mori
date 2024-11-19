using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Bedroom.Scripts;
using Game.Shared.Armario;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour
{
    public BedroomStageController bedroomStageController;

    public ArmarioController armario;
    
    public VolumeProfile sceneVolume;
    public Vignette _vignette;
    
    public LayerMask enemyLayer;
    public LayerMask interactableLayer;
    public LayerMask itemLayer;       // Layer dos itens a serem coletados
    public Light lanternLight;        // Luz da lanterna
    public float focusTime = 3f;      // Tempo necess�rio para coletar o item
    public float enemyFocusTime = 5f;      // Tempo necess�rio para coletar o item
    private float focusProgress = 0f; // Progresso do foco
    private float enemyFocusProgress = 0f; // Progresso do foco

    private GameObject focusedItem;
    private bool isFocusing;

    private void Start()
    {
        if (sceneVolume.TryGet<Vignette>(out var vig))
        {
            _vignette = vig;
        }
    }

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
                lanternLight.spotAngle = Mathf.Lerp(80, 30, focusProgress / focusTime);
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
        
        
        if (Physics.Raycast(ray, out var hit3, lanternLight.range, enemyLayer))
        {
            enemyFocusProgress += Time.deltaTime;

            _vignette.intensity.value = enemyFocusProgress / enemyFocusTime;
            
            if (enemyFocusProgress >= enemyFocusTime)
            {
                enemyFocusProgress = 0f;
                _vignette.intensity.value = 0f;
                StartCoroutine(armario.KillPlayer());
                Destroy(gameObject);
            }
        }
        else
        {
            enemyFocusProgress -= Time.deltaTime * 3f;
            if (enemyFocusProgress < 0f) enemyFocusProgress = 0f;
            _vignette.intensity.value = enemyFocusProgress / enemyFocusTime;
        }
    }

    private void ResetFocus()
    {
        if (focusedItem != null)
        {
            focusedItem.GetComponent<CollectibleItem>().SetRange(0);
        }

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
