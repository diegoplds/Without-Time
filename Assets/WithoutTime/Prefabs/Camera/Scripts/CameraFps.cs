using Dplds.Inputs;
using Dplds.Core;
using UnityEngine;

namespace Dplds.Gameplay
{

    public class CameraFps : MonoBehaviour
    {
        public float MouseY { get => mouseY; set => mouseY = value; }
        public float MouseX { get => mouseX; set => mouseX = value; }
        #region Fields Camera
        [Header("Camera")]
        [SerializeField] private float minY = -65.0f;
        [SerializeField] private float maxY = 65.0f;
        [SerializeField] private float smoothCoefX = 0.3f;
        [SerializeField] private float smoothCoefY = 0.3f;
        private float mouseX;
        private float mouseY;
        private float smoothRotX = 0;
        private float smoothRotY = 0;

        #endregion
        #region Body
        [SerializeField] private Transform head;
        private Transform player;
        #endregion
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void LateUpdate()
        {
            Vector3 newRot =new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,head.eulerAngles.z);//get rotation on head in axis z
            transform.position = head.position;
            transform.localEulerAngles = newRot;
            head.eulerAngles = transform.eulerAngles;

        }
        public void LookAround(ref Vector2 mouse, ref float sens, float multiply = 0, float divide = 0)
        {
            #region Controller
            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.controller)
            {
                smoothRotX = Mathf.Lerp(smoothRotX, mouse.x, smoothCoefX);
                smoothRotY = Mathf.Lerp(smoothRotY, mouse.y, smoothCoefY);
                mouseX += (sens * smoothRotX * multiply * Time.deltaTime);
                mouseY += (sens * smoothRotY * multiply * Time.deltaTime);
                mouseY = Mathf.Clamp(mouseY, minY, maxY);
                player.localEulerAngles = new Vector3(0, mouseX, 0);
                transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

            }
            #endregion
            #region Keyboard
            else
            {
                if (!GameManagement.pause)
                {
                    smoothRotX = Mathf.Lerp(smoothRotX, mouse.x, smoothCoefX);
                    smoothRotY = Mathf.Lerp(smoothRotY, mouse.y, smoothCoefY);
                    mouseX += (sens * smoothRotX * Time.unscaledDeltaTime) / divide / Time.unscaledDeltaTime;
                    mouseY += (sens * smoothRotY * Time.unscaledDeltaTime) / divide / Time.unscaledDeltaTime;
                    mouseY = Mathf.Clamp(mouseY, minY, maxY);
                    player.localEulerAngles = new Vector3(0, mouseX, 0);
                    transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
                }
            }
            #endregion
        }
    }
}
