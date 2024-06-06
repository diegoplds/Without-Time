using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Dplds.Core
{
    public class SceneManagement : MonoBehaviour
    {
        public static SceneManagement Instance { get; private set; }
        [Range(0.01f, 5f)]
        [SerializeField] private float speedFade = 0.5f;
        private CanvasGroup canvasGroup;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
        }
        public void LoadScene(string scene)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadScene(scene);
            }
        }
        public void LoadSceneAsync(string scene)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                StartCoroutine(PerformLoadSceneAsync(scene));
            }
        }
        public void LoadSceneAsyncInGame(string scene)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
        public void UnloadScene(string scene)
        {
            if (SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        public void RestartLevel()
        {
            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
        IEnumerator PerformLoadSceneAsync(string scene)
        {
            yield return StartCoroutine(FadeIn());
            var operation = SceneManager.LoadSceneAsync(scene);
            while (!operation.isDone)
            {
                yield return null;
            }
            yield return StartCoroutine(FadeOut());
        }
        IEnumerator FadeIn()
        {
            GameManagement.pause = true;
            float start = 0;
            float end = 1;
            float speed = (end - start) / speedFade;
            canvasGroup.alpha = start;
            while (canvasGroup.alpha < end)
            {
                canvasGroup.alpha += speed * Time.unscaledDeltaTime;
                yield return null;
            }
            canvasGroup.alpha = end;
        }
        IEnumerator FadeOut()
        {
            float start = 1;
            float end = 0;
            float speed = (end - start) / speedFade;
            canvasGroup.alpha = start;
            while (canvasGroup.alpha > end)
            {
                canvasGroup.alpha += speed * Time.unscaledDeltaTime;
                yield return null;
            }
            canvasGroup.alpha = end;
            GameManagement.pause = false;
        }
    }
}
