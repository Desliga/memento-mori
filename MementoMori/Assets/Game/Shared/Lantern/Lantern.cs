using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Bedroom.Scripts;
using Game.Shared.Armario;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    public float coneRadius = 1000f; // Maximum radius of the cone
    public float coneAngle = 5f; // Half-angle of the cone in degrees

    public BedroomStageController bedroomStageController;
    public BedroomController controller;

    public GameObject enemy;
    public Animator enemyanim;
    public Transform enemypoint;

    public VolumeProfile sceneVolume;
    public Vignette _vignette;

    public LayerMask enemyLayer;
    public LayerMask interactableLayer;
    public LayerMask itemLayer;       // Layer dos itens a serem coletados
    public Light lanternLight;        // Luz da lanterna
    public float focusTime = 3f;      // Tempo necessário para coletar o item
    public float enemyFocusTime = 5f; // Tempo necessário para ataque do inimigo
    private float focusProgress = 0f; // Progresso do foco
    private float enemyFocusProgress = 0f; // Progresso do foco

    private GameObject focusedItem;
    private bool isFocusing;

    public Vector3 initposition;
    public bool canmove;

    private void Start()
    {
        canmove = true;
        initposition = transform.position;
        if (sceneVolume.TryGet<Vignette>(out var vig))
        {
            _vignette = vig;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(lanternLight.transform.position, lanternLight.transform.forward * lanternLight.range);
    }

    void DetectInCone(LayerMask layer, Action<Collider> callback, Action onNotFind = null)
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Collider[] colliders = Physics.OverlapSphere(origin, coneRadius, layer);
        List<Collider> collidersInCone = new List<Collider>();

        foreach (Collider collider in colliders)
        {
            Vector3 toTarget = (collider.transform.position - origin).normalized;

            Debug.DrawRay(origin, collider.transform.position, Color.green);
            Debug.DrawRay(origin, direction, Color.red);
            float angle = Vector3.Angle(direction, toTarget);
            //angle -= 90f;
            //if (angle < 0f) angle = -angle;

            if (angle <= coneAngle)
                collidersInCone.Add(collider);
        }

        foreach (Collider detected in collidersInCone)
        {
            callback(detected);
        }

        if (collidersInCone.Count == 0)
            onNotFind?.Invoke();
    }

    private void Update()
    {
        // Detectar objetos na camada "Item"
        DetectInCone(itemLayer, (collider) =>
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Item")) // Garante que é da camada "Item"
            {
                if (collider.gameObject != focusedItem)
                {
                    ResetFocus();
                    focusedItem = collider.gameObject;
                    isFocusing = true;
                }

                if (isFocusing)
                {
                    focusProgress += Time.deltaTime;
                    lanternLight.spotAngle = Mathf.Lerp(80, 30, focusProgress / focusTime);

                    if (collider.gameObject.TryGetComponent<CollectibleItem>(out var collectibleItem))
                    {
                        collectibleItem.SetRange(focusProgress / focusTime);
                    }

                    if (focusProgress >= focusTime)
                    {
                        CollectItem(focusedItem);
                        ResetFocus();
                    }
                }
            }
        }, () =>
        {
            ResetFocus();
        });
        /*
        // Detectar objetos interagíveis
        DetectInCone(interactableLayer, (collider) =>
        {
            if (collider.gameObject.TryGetComponent<ILanternInteractable>(out var component))
            {
                component.Interact();
            }
        });

        // Detectar inimigos
        DetectInCone(enemyLayer, (collider) =>
        {
            enemyFocusProgress += Time.deltaTime;

            _vignette.intensity.value = enemyFocusProgress / enemyFocusTime;

            if (enemyFocusProgress >= enemyFocusTime)
            {
                enemyFocusProgress = 0f;
                _vignette.intensity.value = 0f;
                StartCoroutine(KillPlayer());
                Destroy(gameObject);
            }
        }, () =>
        {
            enemyFocusProgress -= Time.deltaTime * 3f;
            if (enemyFocusProgress < 0f) enemyFocusProgress = 0f;
            _vignette.intensity.value = enemyFocusProgress / enemyFocusTime;
        });

        // Detectar movimento da lanterna
        if (initposition != transform.position && canmove)
        {
            controller.StartRoom();
            canmove = false;
        }*/
    }

    private void ResetFocus()
    {
        if (focusedItem != null)
        {
            if (focusedItem.TryGetComponent<CollectibleItem>(out var collectibleItem))
            {
                collectibleItem.SetRange(0);
            }
        }

        isFocusing = false;
        focusProgress = 0f;
        focusedItem = null;
        lanternLight.spotAngle = 25; // Ângulo padrão da luz
    }

    private void CollectItem(GameObject item)
    {
        Inventory.Instance.AddItem(item.GetComponent<CollectibleItem>().itemData);
        Destroy(item); // Remove o item da cena

        bedroomStageController.NextStage();
    }

    public IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        enemy.transform.position = enemypoint.position;
        enemyanim.SetTrigger("Jump");
        SoundManager.Instance.PlayScare();
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.GameOver();
    }
}
