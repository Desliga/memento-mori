using System.Collections;
using UnityEngine;

namespace Game.Scenes.Bedroom.Scripts
{
    public class BedroomStageController : MonoBehaviour
    {
        public string nextStage;
        public bool fadeInOnStart;
        
        private void Start()
        {
            if (fadeInOnStart)
                FadeManager.Instance.FadeIn(1.0f);
        }

        [ContextMenu("LoadNext")]
        public void NextStage()
        {
            StartCoroutine(NextStageSequence());
        }

        private IEnumerator NextStageSequence()
        {
            FadeManager.Instance.FadeOut(1.0f);
            yield return new WaitForSeconds(1.0f);
            SoundManager.Instance.PlayGirlLaugh();
            yield return new WaitForSeconds(1.0f);
            GameManager.Instance.LoadScene(nextStage);
        }
    }
}
