using Dplds.Core;
using UnityEngine;
namespace Dplds.General
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private string scene = "menu";
        private float time;
        void FixedUpdate()
        {
            time += Time.deltaTime;
            if (time > 10)
            {
                SceneManagement.Instance.LoadScene(scene);
            }
        }
    }
}
