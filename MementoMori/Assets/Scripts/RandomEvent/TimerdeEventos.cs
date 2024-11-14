using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerdeEventos : MonoBehaviour
{
    public UnityEvent[] Events;
    public float tempoparatest;
    public int chancedeumaem;
    private float timer = 0;
    public int resultadotentativa;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tempoparatest) {
            timer = 0;
            TentativadeEvento();
        }
    }


    public void AtivaEvento()
    {
        int index = Random.Range(0, Events.Length);
        Events[index].Invoke();
    }
    
    private void TentativadeEvento()
    {
        resultadotentativa = Random.Range(1, chancedeumaem+1);
        if (resultadotentativa == chancedeumaem)
        {
            AtivaEvento();
        }
        else
        {
            print("nada");
        }
    }
}
