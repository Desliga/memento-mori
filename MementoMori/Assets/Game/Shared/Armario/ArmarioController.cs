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

        private bool hasLantern = false; // Flag para verificar se o player pegou a lanterna

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (hasLantern)
                RandomOpen();
        }

        private void RandomOpen()
        {
            float time = Random.Range(10f, 20f);
            Invoke(nameof(OpenDoor), time);
        }

        private void OpenDoor()
        {
            if (!hasLantern) return; // Impede a abertura se o player não pegou a lanterna

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

            if (hasLantern)
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

        private void OnCollisionEnter(Collision collision)
        {
            // Verifica se o player pegou a lanterna pela tag
            if (collision.gameObject.CompareTag("Lanterna"))
            {
                hasLantern = true; // Ativa a flag quando o player pega a lanterna
                Debug.Log("Lanterna pegou: Armário pode abrir.");
                RandomOpen(); // Começa o sistema de abertura
            }
        }
    }
}
