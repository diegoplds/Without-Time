using UnityEngine;
namespace Dplds.Core
{
    public class SetParent : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = parent;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.parent = null;
            }
        }
       
    }
}
