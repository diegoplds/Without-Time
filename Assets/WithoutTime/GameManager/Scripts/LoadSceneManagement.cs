using UnityEngine;
namespace Dplds.Core
{
    public class LoadSceneManagement : MonoBehaviour
    {
        private string scene = "";
        private void Start()
        {
            scene = gameObject.name;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManagement.Instance.LoadSceneAsyncInGame(scene);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManagement.Instance.UnloadScene(scene);
            }
        }
    }
}
