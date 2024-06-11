using Dplds.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dplds.Core
{
    public class MenuManagement : MonoBehaviour
    {
        #region Canvas
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private GameObject settingsCanvas;
        [SerializeField] private GameObject creditsCanvas;
        [SerializeField] private GameObject videoSettingsCanvas;
        [SerializeField] private GameObject audioSettingsCanvas;
        [SerializeField] private GameObject controllerSettingsCanvas;
        #endregion

        [SerializeField] private EventSystem eventSystem;
        private void Awake()
        {
            ShowMenu();
            CursorManagement.Instance.ShowCursor(true);
        }

        private void Update()
        {
            if (InputMaster.cancel)
                Back();
        }
        #region UI
        
        public void ShowMenu()
        {
            menuCanvas.SetActive(true);
            creditsCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            controllerSettingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(menuCanvas.transform.GetChild(0).GetChild(0).gameObject);
        }
       
        
        public void ShowSettings()
        {
            menuCanvas.SetActive(false);
            settingsCanvas.SetActive(true);
            videoSettingsCanvas.SetActive(false);
            audioSettingsCanvas.SetActive(false);
            controllerSettingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(settingsCanvas.transform.GetChild(0).GetChild(0).gameObject);
        }
       
       
      
        public void ShowCredits()
        {
            menuCanvas.SetActive(false);
            creditsCanvas.SetActive(true);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(creditsCanvas.transform.GetChild(3).gameObject);
        }
        public void ShowVideoSettings()
        {
            menuCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(videoSettingsCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
        }
        public void ShowAudioSettings()
        {
            menuCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            audioSettingsCanvas.SetActive(true);
            eventSystem.SetSelectedGameObject(audioSettingsCanvas.transform.GetChild(0).GetChild(0).GetChild(1).gameObject);
        }
        public void ShowControllerSettings()
        {
            controllerSettingsCanvas.SetActive(true);
            menuCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
            videoSettingsCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            eventSystem.SetSelectedGameObject(controllerSettingsCanvas.transform.GetChild(0).GetChild(2).gameObject);
        }
     
        public void Back()
        {
            if (!menuCanvas.activeInHierarchy && settingsCanvas.activeInHierarchy||
                !menuCanvas.activeInHierarchy && creditsCanvas.activeInHierarchy)
                ShowMenu();
            else if (audioSettingsCanvas.activeInHierarchy || videoSettingsCanvas.activeInHierarchy||controllerSettingsCanvas.activeInHierarchy)
            {
                ShowSettings();
            } 
        }
        #endregion
        
      public void OpenLink(string link)
        {
            Application.OpenURL(link);
        }
        public void LoadScene(string scene = "menu")
        {
            if (eventSystem.enabled)
                SceneManagement.Instance.LoadSceneAsync(scene);
            eventSystem.enabled = false;
        }
       
    }
}