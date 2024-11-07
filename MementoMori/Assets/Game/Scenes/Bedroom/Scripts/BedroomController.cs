using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scenes.Bedroom.Scripts
{
    public class BedroomController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform targetPosition;
        [SerializeField] private Light elevatorLight;

        private void Start()
        {
            StartCoroutine(MoveSequence());
        }

        private IEnumerator MoveSequence()
        {
            yield return new WaitForSeconds(1.0f);
            BlinkLight(3, 0.0f, 0.1f);
            yield return new WaitForSeconds(1.6f);
            FadeManager.Instance.FadeOut(1.0f);
            yield return new WaitForSeconds(1.0f);
            playerTransform.position = targetPosition.position;
            FadeManager.Instance.FadeIn(1.0f);
        }

        private void BlinkLight(int times, float endValue, float seconds)
        {
            StartCoroutine(BlinkLightCoroutine(times, endValue, seconds));
        }
        
        private IEnumerator BlinkLightCoroutine(int times, float endValue, float seconds)
        {
            float startingValue = elevatorLight.intensity;

            float time = 0;
            for (int i = 0; i < times; i++)
            {
                while (time < seconds)
                {
                    elevatorLight.intensity = Mathf.Lerp(startingValue, endValue, time/seconds);
                    time += Time.deltaTime;
                    yield return null;
                }
                while (time > 0)
                {
                    elevatorLight.intensity = Mathf.Lerp(startingValue, endValue, time/seconds);
                    time -= Time.deltaTime;
                    yield return null;
                }
            }

            elevatorLight.intensity = endValue;
        }
    }
}
