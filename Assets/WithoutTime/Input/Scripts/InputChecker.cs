using Dplds.Core;
using UnityEngine;
namespace Dplds.Inputs
{
    public class InputChecker : MonoBehaviour
    {
        public enum InputDevice { controller = 0, keyboard = 1 };
        public static InputChecker Instance { get; set; }
        public InputDevice inputDevice;
        private InputActions inputActions;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            inputActions = new InputActions();
        }
        private void Update()
        {
            Inputs();
        }
        private void Inputs()
        {
            #region CONTROLLER
            if (GameManagement.Instance.PlayerInput != null)
            {
                if (GameManagement.Instance.PlayerInput.currentControlScheme == "Controller")
                {
                    inputDevice = InputDevice.controller;
                }
            }
            #endregion
            #region KEYBOARD
            //checks if any key on the keyboard has been pressed
            if (GameManagement.Instance.PlayerInput != null)
            {
                if (GameManagement.Instance.PlayerInput.currentControlScheme == "KeyboardMouse")
                {
                    inputDevice = InputDevice.keyboard;
                }
            }
            #endregion
        }
        private void OnEnable()
        {
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
    }
}
