using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Bedroom.Scripts
{
    public class FadeManager : MonoBehaviour
    {
        public static FadeManager Instance;
        
        [Header("References")]
        [SerializeField] private Image fadeImage;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void FadeIn(float seconds)
        {
            StartCoroutine(FadeCoroutine(0.0f, seconds));
        }

        public void FadeOut(float seconds)
        {
            StartCoroutine(FadeCoroutine(1.0f, seconds));
        }

        private IEnumerator FadeCoroutine(float endValue, float seconds)
        {
            float startingAlpha = fadeImage.color.a;
            Color fadeColor = fadeImage.color;
            
            float i = 0;
            while (i < seconds)
            {
                fadeColor.a = Mathf.Lerp(startingAlpha, endValue, i/seconds);
                i += Time.deltaTime;
                fadeImage.color = fadeColor;
                yield return null;
            }

            fadeImage.color = fadeColor;
        }
    }
}
