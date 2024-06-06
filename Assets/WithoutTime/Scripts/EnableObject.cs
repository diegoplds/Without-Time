using UnityEngine;
namespace Dplds.Gameplay
{
    public class EnableObject : MonoBehaviour
    {
        [Tooltip("Enable and disable the same GameObject when enter on trigger and disable when out trigger")]
        [SerializeField] private bool enableDisableWithTrigger = true;
        [Tooltip("disables GameObject as soon as it starts ")]
        [SerializeField] private bool disableWithoutTrigger = false;
        [SerializeField] private GameObject[] enableObjects;
        [SerializeField] private GameObject[] disableObjects;
        private void Awake()
        {
            if (enableDisableWithTrigger)
            {
                if (enableObjects.Length > 0)
                {
                    for (int i = 0; i < enableObjects.Length; i++)
                    {
                        enableObjects[i].SetActive(false);
                    }
                }
            }
            if (disableWithoutTrigger)
            {
                if (disableObjects.Length > 0)
                {
                    for (int i = 0; i < disableObjects.Length; i++)
                    {
                        disableObjects[i].SetActive(false);
                    }
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (enableObjects.Length > 0)
                {
                    for (int i = 0; i < enableObjects.Length; i++)
                    {
                        enableObjects[i].SetActive(true);
                    }
                }
                if (disableObjects.Length > 0)
                {
                    for (int i = 0; i < disableObjects.Length; i++)
                    {
                        disableObjects[i].SetActive(false);
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (enableDisableWithTrigger)
                {
                    if (enableObjects.Length > 0)
                    {
                        for (int i = 0; i < enableObjects.Length; i++)
                        {
                            enableObjects[i].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}