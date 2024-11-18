using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Bedroom.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Shared.Armario
{
    public class ArmarioController : MonoBehaviour, ILanternInteractable
    {
        public GameObject enemy;
        public Animator enemyanim;
        public Transform enemypoint;
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("Open");
        private Coroutine killplayer;
        private bool _open = false;
        private bool canclose;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            RandomOpen();
        }

        private void RandomOpen()
        {
            float time = Random.Range(10f, 20f);
            Invoke(nameof(OpenDoor), time);
        }

        private void OpenDoor()
        {
            canclose = true;
            _animator.SetBool(Open, true);
            _open = true;
            SoundManager.Instance.PlayDoorOpen();
            killplayer = StartCoroutine(KillPlayer());
        }

        public void Interact()
        {
            if (_open == false) return;
            _animator.SetBool(Open, false);
            _open = false;
            SoundManager.Instance.PlayDoorClose();
            if (canclose)
            {
                StopCoroutine(killplayer);
            }
            
            RandomOpen();
        }
        

        private IEnumerator KillPlayer()
        {
            yield return new WaitForSeconds(8.0f);
            canclose = false;
            enemy.transform.position = enemypoint.position;
            enemyanim.SetTrigger("Jump");
            SoundManager.Instance.PlayScare();
            yield return new WaitForSeconds(2.0f);
            GameManager.Instance.GameOver();
            
        }
    }
}
