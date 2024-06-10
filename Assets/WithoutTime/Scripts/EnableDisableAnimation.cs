using UnityEngine;

namespace Dplds.Gameplay
{
    public class EnableDisableAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject[] setActiveObjects;
        public void SetActive(int active)
        {
            if(active == 0)
            {
                for(int i = 0; i < setActiveObjects.Length; i++)
                {
                    setActiveObjects[i].SetActive(false);
                }
            }
            if (active == 1 )
            {
                for (int i = 0; i < setActiveObjects.Length; i++)
                {
                    setActiveObjects[i].SetActive(true);
                }
            }
        }
    }
}
