using UnityEngine;
namespace Dplds.Inputs
{
    public class InputMaster : MonoBehaviour
    {
        public static Vector2 move;
        public static Vector2 mouseFps;
        public static float rewind;
        public static float forward;
        public static bool interact;
        public static float stop;
        public static float run;
        public static bool jump;
        public static bool connect;
        public static bool pause;
        public static bool cancel;
        private InputActions inputActions;
        // Start is called before the first frame update
        void Awake()
        {
            inputActions = new InputActions();
        }
        // Update is called once per frame
        void Update()
        {
            Inputs();
        }
        void Inputs()
        {
            stop = inputActions.Player.Stop.ReadValue<float>();
            interact = inputActions.Player.Interact.triggered;
            move = inputActions.Player.Move.ReadValue<Vector2>();
            jump = inputActions.Player.Jump.triggered;
            forward = inputActions.Player.Forward.ReadValue<float>();
            rewind = inputActions.Player.Rewind.ReadValue<float>();
            mouseFps = inputActions.Player.MouseFps.ReadValue<Vector2>();
            run = inputActions.Player.Run.ReadValue<float>();
            connect = inputActions.UI.x.triggered;
            pause = inputActions.Player.Pause.triggered;
            cancel = inputActions.Player.Cancel.triggered;
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
