using UnityEngine;
using TMPro;
using System;
using Dplds.Core;
using Dplds.Storage;

namespace Dplds.Settings
{
    public class AudioManagement : MonoBehaviour
    {
        public static event Action OnChangeVolume;
        [SerializeField] private TextMeshProUGUI masterVolumeValue;
        [SerializeField] private TextMeshProUGUI musicVolumeValue;
        [SerializeField] private TextMeshProUGUI fxVolumeValue;
        private void Start()
        {
            OnChangeVolume += UpdateVolume;
        }
        #region VOLUME
        public void UpVolume(string namePrefs)
        {
            var volume = PlayerPrefs.GetFloat(namePrefs + GameManagement.key);
            if (volume < 1)
            {
                volume += 0.05f;
                PlayerPrefs.SetFloat(namePrefs + GameManagement.key, volume);
                OnChangeVolume?.Invoke();
            }
        }
        public void DownVolume(string namePrefs)
        {
            var volume = PlayerPrefs.GetFloat(namePrefs + GameManagement.key);
            if (volume > 0)
            {
                volume -= 0.05f;
                PlayerPrefs.SetFloat(namePrefs + GameManagement.key, volume);
                OnChangeVolume?.Invoke();
            }
        }
        void UpdateVolume()
        {
            masterVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.MASTERVOLUME + GameManagement.key) * 100);
            musicVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.MUSICVOLUME + GameManagement.key) * 100);
            fxVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.FXVOLUME + GameManagement.key) * 100);
            AudioListener.volume = PlayerPrefs.GetFloat(NamePrefs.MASTERVOLUME + GameManagement.key);
        }
        #endregion
        private void OnEnable()
        {
            masterVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.MASTERVOLUME + GameManagement.key) * 100);
            musicVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.MUSICVOLUME + GameManagement.key) * 100);
            fxVolumeValue.text = string.Format("{0:00}", PlayerPrefs.GetFloat(NamePrefs.FXVOLUME + GameManagement.key) * 100);
        }
    }
}
