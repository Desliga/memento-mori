using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomRandomAnim : MonoBehaviour
{
    public Animator armario;
    void Start()
    {
        RandomFall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void RandomFall()
    {
        float time = Random.Range(10f, 20f);
        Invoke(nameof(FallSet), time);
    }

    private void FallSet()
    {
        armario.SetTrigger("Fall");
    }
}
