using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternCollect : MonoBehaviour
{
    public bool isCollecting { get; private set; }
    private GameObject _lastFocused;
    
    public void DetectObjects(Camera mainCamera, LayerMask itemLayer)
    {
        Vector3 mousePos = transform.forward;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, itemLayer))
        {
            if (hit.collider.gameObject != _lastFocused)
            {
                CancelInvoke("CollectFocused");
                _lastFocused = hit.collider.gameObject;
                isCollecting = true;
                Invoke("CollectFocused", 3);
            }
        }
        else
        {
            _lastFocused = null;
            CancelInvoke("CollectFocused");
            isCollecting = false;
        }
    }

    private void CollectFocused()
    {
        _lastFocused.GetComponent<ICollect>().Collect();
        isCollecting = false;
    }
}
