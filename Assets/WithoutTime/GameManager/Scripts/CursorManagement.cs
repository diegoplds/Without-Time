using UnityEngine;
namespace Dplds.Inputs
{
    public class CursorManagement : MonoBehaviour
    {
        public static CursorManagement Instance { get; set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        public void ShowCursor(bool showCursor)
        {
            if (showCursor)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
