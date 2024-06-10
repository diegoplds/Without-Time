using Dplds.Core;
using Dplds.Storage;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

namespace Dplds.Settings

{
    public class VideoSettings : MonoBehaviour
    {
        public enum TypeAa { Off, Fxaa, Taa, Smaa }
        public static event Action OnChangeValue;

        [SerializeField] private GameObject[] disableSettings;
        #region Display
        [SerializeField] private TextMeshProUGUI maxFramesValue;
        [SerializeField] private TextMeshProUGUI vsyncValue; 
        [SerializeField] private TextMeshProUGUI allowDynamicResolutionValue;
        [SerializeField] private TextMeshProUGUI dynamicResolutionValue;
        
        [SerializeField] private TextMeshProUGUI AaValue;
        #endregion
        #region Graphics
        [SerializeField] private TextMeshProUGUI motionBlurValue;
        [SerializeField] private TextMeshProUGUI bloomValue;
        [SerializeField] private TextMeshProUGUI ssrValue;
        [SerializeField] private TextMeshProUGUI sunShaftValue; 
        #endregion
        private TypeAa typeAa;
        private int[] maxFrameRate = new int[6] { 30, 60, 120, 144, 240, -1 };
        private int indexAa = 0;
        private int indexMaxFrameRate;
        private int dynamicResolution;
        private GameObject dynamicResolutionGameObject;
        private event Action OnChangeDynamicresolutionValue;
        private void Awake()
        {
            OnChangeValue += LoadValueState;
            indexAa = PlayerPrefs.GetInt(NamePrefs.ANTIALIASING + GameManagement.key);
            indexMaxFrameRate = PlayerPrefs.GetInt(NamePrefs.MAXFRAMES + GameManagement.key);
            dynamicResolution = PlayerPrefs.GetInt(NamePrefs.DYNAMICRESOLUTIONVALUE + GameManagement.key);
            dynamicResolutionGameObject = dynamicResolutionValue.transform.parent.gameObject;
        }
        private void Start()
        {
            LoadValueState();
            UpdateDynamicResolutionValue();
            OnChangeDynamicresolutionValue += UpdateDynamicResolutionValue;
        }
        void LoadValueState()
        {
            #region Display
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
            if (allowDynamicResolutionValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.DYNAMICRESOLUTION + GameManagement.key) == 1)
                {
                    allowDynamicResolutionValue.text = "On";
                    dynamicResolutionGameObject.SetActive(true);
                }
                else
                {
                    allowDynamicResolutionValue.text = "Off";
                    dynamicResolutionGameObject.SetActive(false);
                }
            }
          /*  if (dynamicResolutionValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.DYNAMICRESOLUTIONVALUE + GameManagement.key) == 1)
                {
                    allowDynamicResolutionValue.text = "On";
                }
                else
                {
                    allowDynamicResolutionValue.text = "Off";
                }
            }*/
            #endregion
            #region Graphics
            if (motionBlurValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.MOTIONBLUR + GameManagement.key) == 1)
                {
                    motionBlurValue.text = "On";
                }
                else
                {
                    motionBlurValue.text = "Off";
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
            if (ssrValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.SSR + GameManagement.key) == 1)
                {
                    ssrValue.text = "On";
                }
                else
                {
                    ssrValue.text = "Off";
                }
            }
            if (sunShaftValue != null)
            {
                if (PlayerPrefs.GetInt(NamePrefs.SUNSHAFT + GameManagement.key) == 1)
                {
                    sunShaftValue.text = "On";
                }
                else
                {
                    sunShaftValue.text = "Off";
                }
            }
            #endregion
           
        }
        #region Dynamic resolution
        public void ChangeDynamicResolutionValueUp()
        {
            if (dynamicResolution < 100)
            {
                dynamicResolution += 5;
                //dynamicResolution = indexDynamicResolution;
                PlayerPrefs.SetInt(NamePrefs.DYNAMICRESOLUTIONVALUE+GameManagement.key, dynamicResolution);
                PlayerPrefs.Save();
                OnChangeDynamicresolutionValue?.Invoke();
            }
        }
        public void ChangeDynamicResolutionValueDown()
        {
            if (dynamicResolution > 50)
            {
                dynamicResolution -= 5;
                PlayerPrefs.SetInt(NamePrefs.DYNAMICRESOLUTIONVALUE + GameManagement.key, dynamicResolution);
                PlayerPrefs.Save();
                OnChangeDynamicresolutionValue?.Invoke();
            }
        }
        void UpdateDynamicResolutionValue()
        {
            dynamicResolutionValue.text = dynamicResolution.ToString();

            DynamicResolutionValueMethod();
        }
        private void DynamicResolutionValueMethod()
        {
            RenderPipelineSettings _renderPipelineSettings = GameManagement.Instance.PipelineAsset.currentPlatformRenderPipelineSettings;
            GlobalDynamicResolutionSettings dynamicResolutionSettings = GameManagement.Instance.PipelineAsset.currentPlatformRenderPipelineSettings.dynamicResolutionSettings;
            _renderPipelineSettings.dynamicResolutionSettings = dynamicResolutionSettings;
            _renderPipelineSettings.dynamicResolutionSettings.forcedPercentage = dynamicResolution;
            GameManagement.Instance.PipelineAsset.currentPlatformRenderPipelineSettings = _renderPipelineSettings;

        } 
        #endregion
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
        #region Display
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
        #endregion

        private void OnDestroy()
        {
            OnChangeValue -= LoadValueState;
            OnChangeDynamicresolutionValue -= UpdateDynamicResolutionValue; ;
        }
    }
}
