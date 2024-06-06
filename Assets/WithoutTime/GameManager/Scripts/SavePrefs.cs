using Dplds.Core;
using Dplds.Settings;
using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
namespace Dplds.Storage
{
    public struct NamePrefs
    {
        #region Audio
        public const string MASTERVOLUME = "Master Volume";
        public const string MUSICVOLUME = "Music Volume";
        public const string FXVOLUME = "Fx Volume";
        #endregion
        #region Graphics
        public const string ANTIALIASING = "AaQuality";
        public const string HDR = "Hdr";
        public const string BLOOM = "Bloom";
        public const string AO = "Ao";
        #endregion
        #region Inputs
        public const string CONTROLLERSENS = "Controller Sens";
        public const string MOUSESENS = "Mouse Sens";
        #endregion
        #region frames & vsync
        public const string MAXFRAMES = "Max Frames";
        public const string VSYNC = "Vsync";
        #endregion
    }
    public class SavePrefs : MonoBehaviour
    {
        public static event Action OnChangeVolume;
        private Bloom bloomIsometric;
        private Bloom bloomPerspective;
        private int[] maxFrames = new int[6] { 30, 60, 120, 144, 240, -1 };


       
        void Start()
        {
            Save();
            GetPostProcess();
            Load();
            #region Events
            VideoSettings.OnChangeValue += Load;
            AudioManagement.OnChangeVolume += Load;
            #endregion
        }
        public void Save()
        {
            //save volume
            #region Audio
            #region Master volume
            if (!PlayerPrefs.HasKey(NamePrefs.MASTERVOLUME + GameManagement.key))
            {
                PlayerPrefs.SetFloat(NamePrefs.MASTERVOLUME + GameManagement.key, 1f);
            }
            #endregion
            #region Music volume
            if (!PlayerPrefs.HasKey(NamePrefs.MUSICVOLUME + GameManagement.key))
            {
                PlayerPrefs.SetFloat(NamePrefs.MUSICVOLUME + GameManagement.key, 0.1f);
            }
            #endregion
            #region Fx volume
            if (!PlayerPrefs.HasKey(NamePrefs.FXVOLUME + GameManagement.key))
            {
                PlayerPrefs.SetFloat(NamePrefs.FXVOLUME + GameManagement.key, 1f);
            }
            #endregion
            #endregion
            //save Inputs
            #region Input
            #region Controller
            if (!PlayerPrefs.HasKey(NamePrefs.CONTROLLERSENS + GameManagement.key))
            {
                PlayerPrefs.SetFloat(NamePrefs.CONTROLLERSENS + GameManagement.key, 40);
            }
            #endregion
            #region Keyboard
            if (!PlayerPrefs.HasKey(NamePrefs.MOUSESENS + GameManagement.key))
            {
                PlayerPrefs.SetFloat(NamePrefs.MOUSESENS + GameManagement.key, 10);
            }
            #endregion
            #endregion
            //save locale
            //save Desktop
            #region Desktop
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 5);
                if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 0);
                if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 3);
                if (!PlayerPrefs.HasKey(NamePrefs.HDR + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.HDR + GameManagement.key, 1);
                if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                if (!PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.AO + GameManagement.key, 1);
                QualitySettings.SetQualityLevel(0);//desktop
            }
            #endregion
            // Save XbOne & Save XbSeries
            #region Console
            if (SystemInfo.deviceType == DeviceType.Console)
            {
                #region Xbone
                if (CheckConsole.typeConsole == CheckConsole.TypeConsole.XbOne)
                {
                    if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 1);//60 fps
                    if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 3);
                    if (!PlayerPrefs.HasKey(NamePrefs.HDR + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.HDR + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.AO + GameManagement.key, 1);
                    QualitySettings.SetQualityLevel(0);

                    Screen.SetResolution(1920, 1080, true);
                }
                #endregion
                #region XboneX
                if (CheckConsole.typeConsole == CheckConsole.TypeConsole.XbOneX)
                {
                    if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 3);
                    if (!PlayerPrefs.HasKey(NamePrefs.HDR + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.HDR + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.AO + GameManagement.key, 1);
                    QualitySettings.SetQualityLevel(0);

                    Screen.SetResolution(3840, 2160, true);//4k resolution
                }

                #endregion
                #region XbSeriesS
                if (CheckConsole.typeConsole == CheckConsole.TypeConsole.XbSeriesS)
                {
                    if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 3);
                    if (!PlayerPrefs.HasKey(NamePrefs.HDR + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.HDR + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.AO + GameManagement.key, 1);
                    QualitySettings.SetQualityLevel(0);

                    Screen.SetResolution(2560, 1440, true);//2.5k resolution
                }
                #endregion
                #region XbseriesX
                if (CheckConsole.typeConsole == CheckConsole.TypeConsole.XbSeriesSX)
                {
                    if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 3);
                    if (!PlayerPrefs.HasKey(NamePrefs.HDR + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.HDR + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                    if (!PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
                        PlayerPrefs.SetInt(NamePrefs.AO + GameManagement.key, 1);
                    QualitySettings.SetQualityLevel(0);

                    Screen.SetResolution(3840, 2160, true);//4k resolution
                }
                #endregion
            }
            #endregion
        }
        void GetPostProcess()
        {
            GameManagement.Instance.VolumeProfile[0].TryGet(out bloomIsometric);//isometric
            GameManagement.Instance.VolumeProfile[1].TryGet(out bloomPerspective);//perspective
            

            // ao.SetActive(false);
        }
        public void Load()
        {
            #region Audio
            OnChangeVolume?.Invoke();
            if (PlayerPrefs.HasKey(NamePrefs.MASTERVOLUME + GameManagement.key))
                AudioListener.volume = PlayerPrefs.GetFloat(NamePrefs.MASTERVOLUME + GameManagement.key);
            #endregion
            #region Max Frame Rate
            int indexMaxFrames = PlayerPrefs.GetInt(NamePrefs.MAXFRAMES + GameManagement.key);
            Application.targetFrameRate = maxFrames[indexMaxFrames];
            #endregion
            #region Vsync
            if (PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
            {
                QualitySettings.vSyncCount = PlayerPrefs.GetInt(NamePrefs.VSYNC + GameManagement.key);
            }
            #endregion
            #region bloom
            if (PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
            {
                if (PlayerPrefs.GetInt(NamePrefs.BLOOM + GameManagement.key) == 1)
                {
                    bloomIsometric.active = true;
                    bloomPerspective.active = true;
                }
                else
                {
                    bloomIsometric.active = false;
                    bloomPerspective.active = false;
                }
            }
            #endregion
            #region Ao
           /* if (PlayerPrefs.HasKey(NamePrefs.AO + GameManagement.key))
            {
                if (PlayerPrefs.GetInt(NamePrefs.AO + GameManagement.key) == 1)
                {
                    ao.SetActive(true);
                }
                else
                {
                    ao.SetActive(false);
                }
            }*/
            #endregion
        }
    }
}