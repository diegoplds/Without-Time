using Dplds.Core;
using Dplds.Storage;
using System;
using TMPro;
using UnityEngine;

namespace Dplds.Settings

{
    public class VideoSettings : MonoBehaviour
    {
        public enum TypeAa { Off, Fxaa, Smaa, Taa }
        public static event Action OnChangeValue;
        [SerializeField] private GameObject[] disableSettings;
        [SerializeField] private TextMeshProUGUI maxFramesValue;
        [SerializeField] private TextMeshProUGUI vsyncValue;
        [SerializeField] private TextMeshProUGUI AaValue;
        [SerializeField] private TextMeshProUGUI bloomValue;
        [SerializeField] private TextMeshProUGUI aoValue;
        [SerializeField] private TextMeshProUGUI hdrValue;
        private TypeAa typeAa;
        private int[] maxFrameRate = new int[6] { 30, 60, 120, 144, 240, -1 };
        private int indexAa = 0;
        private int indexMaxFrameRate;
        private void Awake()
        {
            OnChangeValue += LoadValueState;
            indexAa = PlayerPrefs.GetInt(NamePrefs.ANTIALIASING + GameManagement.key);
            indexMaxFrameRate = PlayerPrefs.GetInt(NamePrefs.MAXFRAMES + GameManagement.key);
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                for (int i = 0; i < disableSettings.Length; i++)
                {
                    disableSettings[i].SetActive(false);
                }
            }
            else if (SystemInfo.deviceType == DeviceType.Console)
            {
                for (int i = 0; i < disableSettings.Length; i++)
                {
                    disableSettings[i].SetActive(false);
                }
            }
        }
        private void Start()
        {
            LoadValueState();
        }
        void LoadValueState()
        {
            if (vsyncValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.VSYNC + GameManagement.key) == 1)
                {
                    vsyncValue.text = "On";
                }
                else
                {
                    vsyncValue.text = "Off";
                }
            }
            if (maxFramesValue != null)
            {
                maxFramesValue.text = maxFrameRate[indexMaxFrameRate].ToString();
                if (maxFrameRate[indexMaxFrameRate] == maxFrameRate[5])
                {
                    maxFramesValue.text = "Unlimited";
                }
                //Application.targetFrameRate = maxFrameRate[indexMaxFrameRate];
            }
            if (AaValue != null)
            {
                typeAa = (TypeAa)indexAa;
                AaValue.text = typeAa.ToString();
            }
            if (hdrValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.HDR + GameManagement.key) == 1)
                {
                    hdrValue.text = "On";
                }
                else
                {
                    hdrValue.text = "Off";
                }
            }
            if (bloomValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.BLOOM + GameManagement.key) == 1)
                {
                    bloomValue.text = "On";
                }
                else
                {
                    bloomValue.text = "Off";
                }
            }
            if (aoValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.AO + GameManagement.key) == 1)
                {
                    aoValue.text = "On";
                }
                else
                {
                    aoValue.text = "Off";
                }
            }

        }
        #region PostProcessing
        public void OnChangeQualityGraphics(string nameSettings = "Bloom")
        {

            if (PlayerPrefs.HasKey(nameSettings + GameManagement.key))
            {
                if (PlayerPrefs.GetInt(nameSettings + GameManagement.key) == 1)
                {
                    PlayerPrefs.SetInt(nameSettings + GameManagement.key, 0);
                    OnChangeValue?.Invoke();
                }
                else
                {
                    PlayerPrefs.SetInt(nameSettings + GameManagement.key, 1);
                    OnChangeValue?.Invoke();
                }
            }
        }
        #endregion
        public void MaxFramesDown()
        {
            if (indexMaxFrameRate > 0)
            {
                indexMaxFrameRate--;
                PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, indexMaxFrameRate);

                Application.targetFrameRate = maxFrameRate[indexMaxFrameRate];
                OnChangeValue?.Invoke();
            }
        }
        public void MaxFramesUp()
        {
            if (indexMaxFrameRate < maxFrameRate.Length - 1)
            {
                indexMaxFrameRate++;
                PlayerPrefs.SetInt(NamePrefs.MAXFRAMES + GameManagement.key, indexMaxFrameRate);
                Application.targetFrameRate = maxFrameRate[indexMaxFrameRate];
                OnChangeValue?.Invoke();
            }
        }
        public void ChangeAaUp()
        {
            if (indexAa < Enum.GetNames(typeof(TypeAa)).Length - 1)
            {
                indexAa++;
                PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, indexAa);
                typeAa = (TypeAa)indexAa;
                OnChangeValue?.Invoke();
            }
        }
        public void ChangeAaDown()
        {
            if (indexAa > 0)
            {
                indexAa--;
                PlayerPrefs.SetInt(NamePrefs.ANTIALIASING + GameManagement.key, indexAa);
                typeAa = (TypeAa)indexAa;
                OnChangeValue?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnChangeValue -= LoadValueState;
        }
    }
}
