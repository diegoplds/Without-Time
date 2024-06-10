using UnityEngine;

namespace Dplds.Gameplay
{
    public class DynamicPlatform : PlatformBase
    {
        public byte Id { get => id; }
        [SerializeField] private bool callWithButton = false;
        [SerializeField] private byte id;
        private void Start()
        {
            if (!callWithButton)
            {
                CanMove = true;
            }
        }
        private void Update()
        {
            if (!callWithButton)
            {
                CanMoveMethod();
                Move();
            }
            else
            {
                CallPlatform();
                Move();
            }
        }
       void CallPlatform()
        {
            if(Inventory.idButton.Contains(id))
            {
                CanMoveMethod();
            }
        }
    }
}
