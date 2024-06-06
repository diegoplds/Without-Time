using Dplds.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Dplds.Core
{
    public class PauseManagement : MonoBehaviour
    {
        public static PauseManagement Instance { get; private set; }
        [SerializeField] private GameObject[] disableObjects;
        [Header("UI")]
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject settingsCanvas;
        [SerializeField] private GameObject videoSettingsCanvas;
        [SerializeField] private GameObject AudioSettingsCanvas;
        [SerializeField] private GameObject controllerSettingsCanvas;
        [SerializeField] private EventSystem eventSystem;
        [HideInInspector] public GameObject GetPauseCanvas;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Start()
        {
            #region Console
            if (SystemInfo.deviceType == DeviceType.Console)
            {
                if (disableObjects.Length > 0)
                {
                    for (int i = 0; i < disableObjects.Length; i++)
                    {
                        disableObjects[i].SetActive(false);
                    }
                }
            }
            #endregion
            GetPauseCanvas = pauseCanvas;
            CursorManagement.Instance.ShowCursor(true);
        }
        private void Update()
        {
            if (InputMaster.cancel)
                Back();
        }
        #region UI
        public void ShowPause()
        {
            pauseCanvas.SetActive(true);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            controllerSettingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(pauseCanvas.transform.GetChild(1).GetChild(0).gameObject);
        }
        public void ShowSettings()
        {
            settingsCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            AudioSettingsCanvas.SetActive(false);
            controllerSettingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(settingsCanvas.transform.GetChild(0).GetChild(0).gameObject);
        }
        public void ShowVideoSettings()
        {
            pauseCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            if (videoSettingsCanvas.transform.GetChild(0).GetChild(0).gameObject.activeInHierarchy)
            {
                eventSystem.SetSelectedGameObject(videoSettingsCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
            }
            else
            {
                eventSystem.SetSelectedGameObject(videoSettingsCanvas.transform.GetChild(0).GetChild(1).GetChild(1).gameObject);
            }
           // eventSystem.SetSelectedGameObject(videoSettingsCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
        }
        public void ShowAudioSettings()
        {
            pauseCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            AudioSettingsCanvas.SetActive(true);
            eventSystem.SetSelectedGameObject(AudioSettingsCanvas.transform.GetChild(0).GetChild(1).GetChild(1).gameObject);
        }
        public void ShowControllerSettings()
        {
            controllerSettingsCanvas.SetActive(true);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(controllerSettingsCanvas.transform.GetChild(0).GetChild(2).gameObject);
        }

        public void Back()
        {
            if (!pauseCanvas.activeInHierarchy && settingsCanvas.activeInHierarchy)
                ShowPause();
            else if (AudioSettingsCanvas.activeInHierarchy || videoSettingsCanvas.activeInHierarchy||controllerSettingsCanvas.activeInHierarchy)
            {
                ShowSettings();
            }
        }
        #endregion
        public void LoadScene(string scene = "menu")
        {
            if (eventSystem.enabled)
                SceneManagement.Instance.LoadSceneAsync(scene);
            eventSystem.enabled = false;
        }
    }
}