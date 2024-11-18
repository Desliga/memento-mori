using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongboxController : MonoBehaviour
{
    private Animator animator;
    public GameObject enemy;
    public Animator enemyanim;
    public Transform enemypoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OpenSongbox()
    {
        animator.SetTrigger("open");
        SoundManager.Instance.PlayKillerSong();
        Invoke("CloseSongbox", 15f);
    }

    private void CloseSongbox()
    {
        animator.SetTrigger("close");
        StartCoroutine(KillPlayer());
    }

    private IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(5.0f);
        enemy.transform.position = enemypoint.position;
        enemyanim.SetTrigger("Jump");
        SoundManager.Instance.PlayScare();
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.GameOver();
    }
}
