using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class lighttest : MonoBehaviour
{

    /* 
     * Codigo copiado da lanterna que coleta itens e modificado para afetar os monstros
     * A unica coisa que é pode ser utilizado na lanterna original é a função for no update para afetar os monstros tbm
     
     */
    [SerializeField] private Light luz;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask interagiveis;

    [SerializeField] private string layermonstroarmario;
    [SerializeField] private string layercoelho;
    // Start is called before the first frame update
    void Start()
    {
        
        luz.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            luz.enabled = !luz.enabled;
        }
        if (luz.enabled)
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interagiveis))
            {

                
                switch (hit.collider.gameObject.layer)
                {
                    case int n when (n == LayerMask.NameToLayer(layermonstroarmario)):
                        Monstro cube = hit.collider.gameObject.GetComponent<Monstro>();
                        if (cube != null)
                        {
                            cube.StopTimer();
                        }
                        break;
                    case int n when (n == LayerMask.NameToLayer(layercoelho)):
                        Rabbit coelho = hit.collider.gameObject.GetComponent<Rabbit>();
                        if (coelho != null)
                        {
                            coelho.CoelhoDesativado();
                        }


                        break;
                }
                
            }
        }
        
    }
}
