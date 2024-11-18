using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [Header("Som 1 - Correndo")]
    public AudioClip runningSound;   // Som da pessoa correndo
    public float minInterval1 = 15f; // Intervalo m�nimo para o som 1
    public float maxInterval1 = 20f; // Intervalo m�ximo para o som 1

    [Header("Som 2 - Grito ou Outro Som")]
    public AudioClip secondSound;   // Outro som (ex: grito ou ambiente assustador)
    public float minInterval2 = 20f; // Intervalo m�nimo para o som 2
    public float maxInterval2 = 30f; // Intervalo m�ximo para o som 2

    private AudioSource audioSource;
    private bool isPlayingSound = false; // Garante que apenas um som toque por vez

    private void Start()
    {
        // Certifique-se de que o AudioSource est� configurado
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Define configura��es padr�o do AudioSource
        audioSource.playOnAwake = false; // O som n�o toca ao iniciar
        audioSource.loop = false;       // O som n�o � repetido automaticamente

        // Inicia as corrotinas para tocar os sons
        StartCoroutine(PlayRandomSound(runningSound, minInterval1, maxInterval1));
        StartCoroutine(PlayRandomSound(secondSound, minInterval2, maxInterval2));
    }

    private IEnumerator PlayRandomSound(AudioClip soundClip, float minInterval, float maxInterval)
    {
        while (true)
        {
            // Espera um tempo aleat�rio
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Toca o som somente se nenhum outro est� sendo reproduzido
            if (!isPlayingSound && soundClip != null)
            {
                isPlayingSound = true;
                audioSource.clip = soundClip;
                audioSource.Play();

                // Espera o som terminar antes de liberar para o pr�ximo
                yield return new WaitForSeconds(soundClip.length);
                isPlayingSound = false;
            }
        }
    }
}
