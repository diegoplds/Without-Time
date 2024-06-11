using Dplds.Core;
using UnityEngine;

namespace Dplds
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string scene = "End";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManagement.Instance.LoadSceneAsync(scene);
            }
        }
    }
}
