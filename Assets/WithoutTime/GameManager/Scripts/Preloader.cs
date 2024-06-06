using UnityEngine;
namespace Dplds.Core
{
    public class Preloader : MonoBehaviour
    {
        [Range(0.001f,10)]
        [SerializeField] private float speedAlpha = 2.0f;
        [SerializeField] private float timeToLoad = 5;
        [SerializeField] private string scene = "Menu";
        [SerializeField] private CanvasGroup canvasGroup;
        private float time;
        private float alpha;
        private void Update()
        {
            time += Time.deltaTime;
            if ((time>timeToLoad))
            {
              alpha =  AlphaCanvas();
                if (alpha <= 0)
                {
                    LoadScene(scene);
                }
            }
        }
        float AlphaCanvas()
        {
            return canvasGroup.alpha -= Time.deltaTime*speedAlpha;
        }
      
        void LoadScene(string scene)
        {
            SceneManagement.Instance.LoadScene(scene);
        }
    }
}
