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
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource jumpScare;
    [SerializeField] private AudioSource chuva;
    [SerializeField] private AudioSource trovao;
    [SerializeField] private AudioSource songbox;
    [SerializeField] private AudioSource fallpaper;
    [SerializeField] private AudioSource fallobj;

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
        doorOpen.Play();
    }
   
    public void PlayDoorClose()
    {
        doorClose.Play();
    }

    public void PlayScare()
    {
        jumpScare.Play();
    }

    public void PlayKillerSong()
    {
        songbox.Play();
    }
    public void PlayFall()
    {
        fallpaper.Play();
        fallobj.Play();
    }
   
}
