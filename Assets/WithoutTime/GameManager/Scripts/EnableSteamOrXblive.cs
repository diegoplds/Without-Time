using UnityEngine;
namespace Dplds.Settings
{
    public class EnableSteamOrXblive : MonoBehaviour
    {
        [SerializeField] private GameObject[] steamGameObjects;
        [SerializeField] private GameObject[] xbiveGameObjects;
        void Awake()
        {
            #region Steam
#if UNITY_STANDALONE
        for (int i = 0; i < steamGameObjects.Length; i++)
        {
            steamGameObjects[i].SetActive(true);
        } 
#endif
            #endregion
            #region Wsa
#if UNITY_WSA
            for (int i = 0; i < xbiveGameObjects.Length; i++)
            {
                xbiveGameObjects[i].SetActive(true);
            }
#endif
            #endregion
        }
    }
}
