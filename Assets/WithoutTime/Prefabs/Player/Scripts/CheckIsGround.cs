using Dplds.Core;
using UnityEngine;

namespace Dplds.Gameplay
{
    public class CheckIsGround : MonoBehaviour
    {
        public bool IsGround { get => isGround;}
        private bool isGround;
        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<CustomTag>())
            {
                CustomTag tags = other.GetComponent<CustomTag>();
                if (tags.tags.Contains("Ground"))
                {
                    isGround = true;
                }
               
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<CustomTag>())
            {
                CustomTag tags = other.GetComponent<CustomTag>();
                if (tags.tags.Contains("Ground"))
                {
                    isGround = false;
                }

            }
        }
    }
}
