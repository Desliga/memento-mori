using System;
using Game.Scenes.Bedroom.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Shared.Armario
{
    public class ArmarioController : MonoBehaviour, ILanternInteractable
    {
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("Open");

        private bool _open = false;
        
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
            _animator.SetBool(Open, true);
            _open = true;
        }

        public void Interact()
        {
            if (_open == false) return;
            
            _animator.SetBool(Open, false);
            _open = false;
            
            RandomOpen();
        }
    }
}
