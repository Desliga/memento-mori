using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstro : MonoBehaviour
{
    [SerializeField] private float mintempoparajumpscare;
    [SerializeField] private float maxtempoparajumpscare;
    [SerializeField] private bool estaabrindo = false;
    [SerializeField] private float mintempodereset;
    [SerializeField] private float maxtempodereset;
    private float resettimer;
    private float jumpscaretimer;

    public Material evil;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        resettimer = Random.Range(mintempodereset, maxtempodereset);
        jumpscaretimer = Random.Range(mintempoparajumpscare, maxtempoparajumpscare);

        rend = GetComponent<Renderer>();
        rend.enabled = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!estaabrindo)
        {
            resettimer -= Time.deltaTime;
            if (resettimer <= 0)
            {
                resettimer = Random.Range(mintempodereset, maxtempodereset);
                estaabrindo = true;
                print("Comecou");
                rend.enabled=true;
            }
        }

        if (estaabrindo)
        {
            jumpscaretimer -= Time.deltaTime;
            
            if (jumpscaretimer <= 0)
            {
                Jumpscare();
                
            }
        }
    }

    public void Jumpscare()
    {
        print("Jumpscare");
        estaabrindo = false;
        rend.material = evil;
    }

    public void StopTimer()
    {
        estaabrindo = false;
        jumpscaretimer = Random.Range(mintempoparajumpscare, maxtempoparajumpscare);
        print("Parou timer");
        rend.enabled = false;
    }
}
