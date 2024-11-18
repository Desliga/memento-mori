using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("References")] 
    [SerializeField] private AudioSource girlLaugh;
    [SerializeField] private AudioSource lightSpark;
    [SerializeField] private AudioSource DoorOpen;
    [SerializeField] private AudioSource DoorClose;
    
   private void Awake()
   {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
               
        DontDestroyOnLoad(gameObject);
   }

   public void PlayGirlLaugh()
   {
       girlLaugh.Play();
   }
   
   public void PlayLightSpark()
   {
       lightSpark.Play();
   }

   public void PlayDoorOpen()
   {
       DoorOpen.Play();
   }
   
   public void PlayDoorClose()
   {
       DoorClose.Play();
   }
   
}
