using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Shared.Armario
{
    public class ArmarioController : MonoBehaviour
    {
        public GameObject enemy;
        public Animator enemyanim;
        public Transform enemypoint;
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("Open");
        private Coroutine killplayer;
        private bool _open = false;
        private bool canclose;

        public Transform lantern; // Referência à lanterna
        private Vector3 lastLanternPosition; // Última posição registrada da lanterna
        private bool randomOpenTriggered = false; // Flag para evitar múltiplas chamadas

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (lantern != null)
            {
                lastLanternPosition = lantern.position; // Registra a posição inicial da lanterna
            }
        }

        private void Update()
        {
            if (lantern != null && !randomOpenTriggered)
            {
                // Verifica se a posição da lanterna mudou
                if (Vector3.Distance(lantern.position, lastLanternPosition) > 0.01f)
                {
                    randomOpenTriggered = true; // Garante que RandomOpen só será chamado uma vez
                    RandomOpen();
                }
            }
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

        public IEnumerator KillPlayer()
        {
            yield return new WaitForSeconds(8.0f);
            canclose = false;
            enemy.transform.position = enemypoint.position;
            SoundManager.Instance.PlayScare();
            yield return new WaitForSeconds(2.0f);
            GameManager.Instance.GameOver();
        }
    }
}
