using UnityEngine;
using TMPro;
using Dplds.Core;
using Dplds.Storage;

namespace Dplds.Inputs
{
    public class ControllerManagement : MonoBehaviour
    {
        public static event System.Action OnChangeSens;
        [SerializeField] private TextMeshProUGUI controllerSensValue;
        [SerializeField] private TextMeshProUGUI mouseSensValue;
        [SerializeField] private GameObject mouseGroup;
        private void Awake()
        {
            ShowMouseSettings();
            UpdateValue();
        }
        private void Start()
        {
            OnChangeSens += UpdateValue;
        }
        #region SENSITIVITY
        public void UpSens(string namePrefs)
        {
            var sens = PlayerPrefs.GetFloat(namePrefs + GameManagement.key);
            if (sens < 100)
            {
                sens += 2.0f;
                PlayerPrefs.SetFloat(namePrefs + GameManagement.key, sens);
                OnChangeSens?.Invoke();
            }
        }
        public void DownSens(string namePrefs)
        {
            var sens = PlayerPrefs.GetFloat(namePrefs + GameManagement.key);
            if (sens > 0)
            {
                sens -= 2.0f;
                PlayerPrefs.SetFloat(namePrefs + GameManagement.key, sens);
                controllerSensValue.text = sens.ToString();
                OnChangeSens?.Invoke();
            }
        }
        void UpdateValue()
        {
            controllerSensValue.text = PlayerPrefs.GetFloat(NamePrefs.CONTROLLERSENS + GameManagement.key).ToString();
            mouseSensValue.text = PlayerPrefs.GetFloat(NamePrefs.MOUSESENS + GameManagement.key).ToString();
        }
        #endregion
        void ShowMouseSettings()
        {
            if (SystemInfo.deviceType == DeviceType.Console)
                mouseGroup.SetActive(false);
        }
        
    }
}