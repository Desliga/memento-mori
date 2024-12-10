using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Bedroom.Scripts;
using UnityEngine;

public class SongboxController : MonoBehaviour, ILanternInteractable
{
    private Animator animator;
    public GameObject enemy;
    public Animator enemyanim;
    public Transform enemypoint;
    public Transform enemypoin2;

    private bool canClear = false;
    private Coroutine closeCoroutine;

    private int clearCount = 0;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OpenSongbox()
    {
        animator.SetTrigger("open");
        closeCoroutine = StartCoroutine(CloseCoroutine());
    }

    private IEnumerator CloseCoroutine()
    {
        SoundManager.Instance.PlayKillerSong();
        yield return new WaitForSeconds(5.0f);
        canClear = true;
        yield return new WaitForSeconds(10.0f);
        animator.SetTrigger("close");
        StartCoroutine(KillPlayer());
    }

    private IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(5.0f);
        enemy.transform.parent = enemypoint;
        enemy.transform.localPosition = Vector3.zero;
        enemy.transform.localRotation = Quaternion.identity;
        enemyanim.SetTrigger("Jump");
        SoundManager.Instance.PlayScare();
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.GameOver();
    }

    public void Interact()
    {
        if (canClear)
        {
            canClear = false;
            SoundManager.Instance.songbox.Stop();
            clearCount++;
            if (clearCount == 3)
            {
                SoundManager.Instance.PlayBreak();
                enemy.transform.position = enemypoin2.position;
                SoundManager.Instance.Rewind();
                StopCoroutine(closeCoroutine);
                Invoke(nameof(Rewind), 0.8f);
            }
            else if (clearCount == 5)
            {
                StartCoroutine(KillPlayer());
            }
            else
            {
                SoundManager.Instance.Rewind();
                StopCoroutine(closeCoroutine);
                Invoke(nameof(Rewind), 0.8f);
            }
        }
    }

    private void Rewind()
    {
        closeCoroutine = StartCoroutine(CloseCoroutine());
    }
}
