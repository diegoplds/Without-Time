using Dplds.Core;
using Dplds.Gameplay;
using Dplds.Storage;
using UnityEngine;
namespace Dplds.Inputs
{
    public class Inputs : MonoBehaviour
    {
        [SerializeField] private float multiply = 10.0f;
        [SerializeField] private float divide = 175.0f;
        private float sensMouse = 10.0f;
        private float sensController = 45.0f;
        private Movement movement;
        private CameraFps cameraFps;
        private Interact interact;
        private Weapon weapon;
        void Awake()
        {
            movement = GetComponent<Movement>();
            cameraFps = Camera.main.GetComponent<CameraFps>();
            weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
            interact = cameraFps.GetComponent<Interact>();
        }
        private void Start()
        {
            UpdateSens();
            ControllerManagement.OnChangeSens += UpdateSens;
        }
        private void Update()
        {
            GameManagement.Instance.Pause(ref InputMaster.pause, ref InputMaster.cancel);
            movement.HandleMovement(ref InputMaster.move, ref InputMaster.run, ref InputMaster.jump);
            weapon.BackTime(ref InputMaster.rewind, ref InputMaster.forward,ref InputMaster.stop);
            interact.CheckObjectInteractable(ref InputMaster.interact);
        }
        private void LateUpdate()
        {
            #region Keyboard

            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.keyboard)
            {
                cameraFps.LookAround(ref InputMaster.mouseFps, ref sensMouse, multiply: 0, divide);
            } 
            #endregion
            #region Controller
            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.controller)
            {
                cameraFps.LookAround(ref InputMaster.mouseFps, ref sensController, multiply, divide: 0);
            }
            #endregion
        }
        void UpdateSens()
        {
            #region Keyboard
            if (PlayerPrefs.HasKey(NamePrefs.MOUSESENS + GameManagement.key))
            {
                sensMouse = PlayerPrefs.GetFloat(NamePrefs.MOUSESENS + GameManagement.key);
            }
            #endregion
            #region Controller
            if (PlayerPrefs.HasKey(NamePrefs.CONTROLLERSENS + GameManagement.key))
            {
                sensController = PlayerPrefs.GetFloat(NamePrefs.CONTROLLERSENS + GameManagement.key);
            }
            #endregion
        }
    }
}
