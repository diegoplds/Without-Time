using Dplds.Inputs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
namespace Dplds.Core
{
    public class GameManagement : MonoBehaviour
    {
        #region Properties
        public PlayerInput PlayerInput { get => playerInput; }
        public VolumeProfile[] VolumeProfile { get => volumeProfiles; }
        public HDRenderPipeline RendererData { get => rendererData; }
        #endregion
        #region Events
        public static event Action OnPause;
        public static event Action OnUnpause;
        #endregion
        #region Statics
        public static GameManagement Instance { get; private set; }
        public static string key = "Player";
        public static bool pause;
        #endregion

        [Header("Game Manager")]
        [SerializeField] private VolumeProfile[] volumeProfiles;
        [SerializeField] private HDRenderPipeline rendererData;
        private PlayerInput playerInput;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            TryGetComponent(out playerInput);
            SavePrefsResolution();
            GetResolutionPrefs();

            
            pause = false;
        }
        private void Update()
        {
            TimeScale();
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.J))
                Application.targetFrameRate = 30;
            if (Input.GetKeyDown(KeyCode.K))
                Application.targetFrameRate = 60;
            if (Input.GetKeyDown(KeyCode.L))
                Application.targetFrameRate = -1;
#endif
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
#endif
            #endregion
        }
        void GetResolutionPrefs()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (PlayerPrefs.HasKey("width") && PlayerPrefs.HasKey("height"))
                {
                    #region WSA
#if UNITY_WSA
                    #region RESOLUTION
                    Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"), true);
                    #endregion
#endif
                    #endregion
                }
            }
        }
        #region PAUSE
        void TimeScale()
        {
            if (pause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        public void Pause(ref bool pauseButton, ref bool cancel)
        {
            if (pauseButton && !SceneManager.GetSceneByName("Pause").isLoaded)
            {
                if (!SceneManager.GetSceneByName("Menu").isLoaded)
                {
                    SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
                    pause = true;
                    OnPause?.Invoke();
                }
            }
            else if (pauseButton && SceneManager.GetSceneByName("Pause").isLoaded || cancel && SceneManager.GetSceneByName("Pause").isLoaded)
            {
                if (PauseManagement.Instance != null)
                {
                    if (PauseManagement.Instance.GetPauseCanvas.activeSelf)
                    {
                        SceneManager.UnloadSceneAsync("Pause");
                        pause = false;
                        CursorManagement.Instance.ShowCursor(false);
                        OnUnpause?.Invoke();
                    }
                }
            }
        }
        #endregion
        public void Exit()
        {
            Application.Quit();
        }
    }
}