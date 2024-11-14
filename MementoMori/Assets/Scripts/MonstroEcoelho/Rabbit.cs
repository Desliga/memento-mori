using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    Animator animator;
    bool estaativo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CoelhoAtivado();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CoelhoAtivado()
    {
        estaativo = true;
        animator.SetBool("Ativo", estaativo);
    }

    public void CoelhoDesativado()
    {
        estaativo = false;
        animator.SetBool("Ativo", estaativo);
        Invoke("CoelhoAtivado", Random.Range(4f, 10f));
    }
}
