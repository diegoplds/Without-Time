using System;
using TMPro;
using UnityEngine;
namespace Dplds.Settings
{
    public class SettingsResolution : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueResolution;
        private Resolution[] resolutions;
        private int currentResolution = 0;
        private event Action OnResolutionChanged;
        private void Awake()
        {
            resolutions = Screen.resolutions;
            SavePrefsResolution();
        }
        void Start()
        {
            GetCurrentResolution();
            UpdateResolutionInfo();
            OnResolutionChanged += UpdateResolutionInfo;
        }
        void SavePrefsResolution()
        {
            #region WSA
#if UNITY_WSA
            if (!PlayerPrefs.HasKey("width"))
            {
                PlayerPrefs.SetInt("width", Screen.resolutions[Screen.resolutions.Length - 1].width);
            }
            if (!PlayerPrefs.HasKey("height"))
            {
                PlayerPrefs.SetInt("height", Screen.resolutions[Screen.resolutions.Length - 1].height);
            }
            /*  if (!PlayerPrefs.HasKey("fullscreenmode"))
              {
                  PlayerPrefs.SetInt("fullscreenmode", currentFullScreenMode);
              }*/
#endif
            #endregion
        }
        void UpdateResolutionInfo()
        {
            #region WSA
#if UNITY_WSA
            valueResolution.text = Screen.resolutions[currentResolution].width + "X" + Screen.resolutions[currentResolution].height;
            //valueScreenMode.text = fullScreenMode.ToString();
#endif
            #endregion
            #region Standalone & android
#if UNITY_STANDALONE || UNITY_ANDROID
            valueResolution.text = Screen.resolutions[currentResolution].width + "X" + Screen.resolutions[currentResolution].height + Screen.resolutions[currentResolution].refreshRateRatio +" HZ";
        //valueScreenMode.text = fullScreenMode.ToString();
#endif
            #endregion
        }
        void GetCurrentResolution()
        {
            #region WSA
#if UNITY_WSA
            #region RESOLUTION
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width.Equals(PlayerPrefs.GetInt("width"))
                    && resolutions[i].height.Equals(PlayerPrefs.GetInt("height")))
                {
                    currentResolution = i;
                }
            }
            #endregion
#endif
            #endregion
            #region Standalone & android
#if UNITY_STANDALONE || UNITY_ANDROID
            #region RESOLUTION
            for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width.Equals(Screen.currentResolution.width)
                && resolutions[i].height.Equals(Screen.currentResolution.height))
            {
                currentResolution = i;
            }
        }
            #endregion
#endif
            #endregion
        }
        public void ChangeResolutionUp()
        {
            if (currentResolution < resolutions.Length - 1)
            {
                currentResolution += 1;
                Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, true);
                #region WSA
#if UNITY_WSA
                PlayerPrefs.SetInt("width", resolutions[currentResolution].width);
                PlayerPrefs.SetInt("height", resolutions[currentResolution].height);
                PlayerPrefs.Save();
#endif
                #endregion
                OnResolutionChanged?.Invoke();
            }
        }
        public void ChangeResolutionDown()
        {
            if (currentResolution != 0)
            {
                currentResolution -= 1;
                Screen.SetResolution(resolutions[currentResolution].width, resolutions[currentResolution].height, true);
                #region WSA
#if UNITY_WSA
                PlayerPrefs.SetInt("width", resolutions[currentResolution].width);
                PlayerPrefs.SetInt("height", resolutions[currentResolution].height);
                PlayerPrefs.Save();
#endif
                #endregion
                OnResolutionChanged?.Invoke();
            }
        }
        private void OnDestroy()
        {
            OnResolutionChanged -= UpdateResolutionInfo;
        }
    }
}