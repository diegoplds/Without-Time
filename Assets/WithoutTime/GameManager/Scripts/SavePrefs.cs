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
  
        public const string DYNAMICRESOLUTION = "Dynamic Resolution";
        public const string DYNAMICRESOLUTIONVALUE = "Dynamic Resolution Value";
        public const string BLOOM = "Bloom";
        public const string SSR = "Ssr";
        public const string MOTIONBLUR = "Motion Blur";
        public const string SUNSHAFT = "Sun Shaft";
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
        private ScreenSpaceReflection ssr;
        private Bloom bloom;
        private Fog sunShaft;
        private MotionBlur motionBlur;

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
            //save Desktop
            #region Desktop
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (!PlayerPrefs.HasKey(NamePrefs.MAXFRAMES + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, 5);//Unlimited
                //
                if (!PlayerPrefs.HasKey(NamePrefs.VSYNC + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.VSYNC + GameManagement.key, 0);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.ANTIALIASING + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, 2);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.DYNAMICRESOLUTION + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.DYNAMICRESOLUTION + GameManagement.key, 0);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.DYNAMICRESOLUTIONVALUE + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.DYNAMICRESOLUTIONVALUE + GameManagement.key, 70);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.BLOOM + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.BLOOM + GameManagement.key, 1);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.SSR + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.SSR + GameManagement.key, 1);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.MOTIONBLUR + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.MOTIONBLUR + GameManagement.key, 1);
                //
                if (!PlayerPrefs.HasKey(NamePrefs.SUNSHAFT + GameManagement.key))
                    PlayerPrefs.SetInt(NamePrefs.SUNSHAFT + GameManagement.key, 1);
                QualitySettings.SetQualityLevel(0);//desktop
            }
            #endregion
        }
        void GetPostProcess()
        {
            GameManagement.Instance.VolumeProfile.TryGet(out bloom);
            GameManagement.Instance.VolumeProfile.TryGet(out motionBlur);
            GameManagement.Instance.VolumeProfile.TryGet(out ssr);
            GameManagement.Instance.VolumeProfile.TryGet(out sunShaft);


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
                bloom.active = Convert.ToBoolean(PlayerPrefs.GetInt(NamePrefs.BLOOM + GameManagement.key));
            }
            #endregion
            #region motion Blur
            if (PlayerPrefs.HasKey(NamePrefs.MOTIONBLUR + GameManagement.key))
            {
                motionBlur.active = Convert.ToBoolean(PlayerPrefs.GetInt(NamePrefs.MOTIONBLUR + GameManagement.key));
            }
            #endregion
            #region ssr
            if (PlayerPrefs.HasKey(NamePrefs.SSR + GameManagement.key))
            {
                ssr.enabled.value = Convert.ToBoolean(PlayerPrefs.GetInt(NamePrefs.SSR + GameManagement.key));
            }
            #endregion
            #region sun shaft
            if (PlayerPrefs.HasKey(NamePrefs.SUNSHAFT + GameManagement.key))
            {
                sunShaft.active = Convert.ToBoolean(PlayerPrefs.GetInt(NamePrefs.SUNSHAFT + GameManagement.key));
            }
            #endregion

        }
    }
}