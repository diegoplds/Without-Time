using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using Dplds.Inputs;
using UnityEngine.UI;
namespace Dplds.Core
{
    public class ButtonSelected : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler/*, IPointerDownHandler, IPointerUpHandler*/
    {
        [SerializeField] private Color colorSelected = new Color(0, 1, 0, 1);
        [SerializeField] private Color colorDeselected = new Color(0, 0, 0, 1);
        private PointerEventData eventData = null;
        private EventSystem eventSystem = null;
        private InputSystemUIInputModule inputSystemUI;
        private Button button;
        void Awake()
        {
            inputSystemUI = GameObject.Find("EventSystem")?.GetComponent<InputSystemUIInputModule>();
            eventSystem = GameObject.Find("EventSystem")?.GetComponent<EventSystem>();
            eventData = new PointerEventData(eventSystem);
            button = GetComponent<Button>();
        }
        void Update()
        {
            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.controller)
            {
                if (eventData.selectedObject != null)
                {
                    if (eventData.selectedObject.name == gameObject.name)
                        GetComponentInChildren<TextMeshProUGUI>().color = colorSelected;
                    else
                        GetComponentInChildren<TextMeshProUGUI>().color = colorDeselected;
                }
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {

            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.keyboard)
            {
                if (!button.interactable)
                    return;
                GetComponentInChildren<TextMeshProUGUI>().color = colorSelected;
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (InputChecker.Instance.inputDevice == InputChecker.InputDevice.keyboard)
            {
                if (!button.interactable)
                    return;
                GetComponentInChildren<TextMeshProUGUI>().color = colorDeselected;
            }
        }
    }
}
