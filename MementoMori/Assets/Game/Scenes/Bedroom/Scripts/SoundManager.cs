using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("References")] 
    [SerializeField] private AudioSource girlLaugh;
    [SerializeField] private AudioSource lightSpark;
    
   private void Awake()
           {
               if (Instance == null)
                   Instance = this;
               else
                   Destroy(gameObject);
           }

   public void PlayGirlLaugh()
   {
       girlLaugh.Play();
   }
   
   public void PlayLightSpark()
   {
       lightSpark.Play();
   }

}
