using UnityEngine;
using UnityEngine.InputSystem.UI;
namespace Dplds.Inputs
{
    public class EventSystemController : MonoBehaviour
    {
        private InputSystemUIInputModule inputSystemUI;
        private float time;
        // Start is called before the first frame update
        private void Awake()
        {
            inputSystemUI = GetComponent<InputSystemUIInputModule>();
        }
        void Start()
        {
            inputSystemUI.enabled = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (!inputSystemUI.enabled)
            {
                time += Time.deltaTime;
                if (time >= 0.7f)
                {
                    inputSystemUI.enabled = true;
                }
            }
        }
    }
}