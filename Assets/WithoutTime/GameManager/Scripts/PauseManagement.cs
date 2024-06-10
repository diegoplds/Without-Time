using Dplds.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dplds.Core
{
    public class PauseManagement : MonoBehaviour
    {
        public static PauseManagement Instance { get; private set; }
        #region Canvas
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject settingsCanvas;
        [SerializeField] private GameObject videoSettingsCanvas;
        [SerializeField] private GameObject audioSettingsCanvas;
        [SerializeField] private GameObject controllerSettingsCanvas;
        [HideInInspector] public GameObject GetPauseCanvas;
        #endregion

        [SerializeField] private EventSystem eventSystem;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
        }
        private void Start()
        {
            GetPauseCanvas = pauseCanvas;
            ShowPause();
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
            eventSystem.SetSelectedGameObject(pauseCanvas.transform.GetChild(0).GetChild(0).gameObject);
        }


        public void ShowSettings()
        {
            pauseCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            videoSettingsCanvas.SetActive(false);
            audioSettingsCanvas.SetActive(false);
            controllerSettingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(settingsCanvas.transform.GetChild(0).GetChild(0).gameObject);
        }



        
        public void ShowVideoSettings()
        {
            pauseCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(videoSettingsCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
        }
        public void ShowAudioSettings()
        {
            pauseCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            audioSettingsCanvas.SetActive(true);
            eventSystem.SetSelectedGameObject(audioSettingsCanvas.transform.GetChild(0).GetChild(1).GetChild(1).gameObject);
        }
        public void ShowControllerSettings()
        {
            controllerSettingsCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(controllerSettingsCanvas.transform.GetChild(0).GetChild(2).gameObject);
        }

        public void Back()
        {
            if (!pauseCanvas.activeInHierarchy && settingsCanvas.activeInHierarchy)
                ShowPause();
            else if (audioSettingsCanvas.activeInHierarchy || videoSettingsCanvas.activeInHierarchy || controllerSettingsCanvas.activeInHierarchy)
            {
                ShowSettings();
            }
        }
        #endregion


        public void LoadScene(string scene = "pause")
        {
            if (eventSystem.enabled)
                SceneManagement.Instance.LoadSceneAsync(scene);
            eventSystem.enabled = false;
        }

    }
}