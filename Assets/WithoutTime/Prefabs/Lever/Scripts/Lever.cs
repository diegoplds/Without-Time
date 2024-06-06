using Dplds.Core;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform leverTransform;
        [SerializeField] private Vector3 rotAxis = new(1, 0, 0);
        [SerializeField] private int idDoor;
        [SerializeField] private int minRot = 45;
        [SerializeField] private int maxRot = 120;
        private int dir;
        private AnimatorBehaviour animatorBehaviour;
        void Awake ()
        {
            animatorBehaviour = GetComponent<AnimatorBehaviour>();
        }
        void Start()
        {
            InitLever();
        }
        public void Interact()
        {
            if ((animatorBehaviour.CurrentAnimation<=0))
            {
                
                InitLever();
            }
        }
        void InitLever()
        {
            //Close door
            if (dir == 0)
            {
                dir = 1;
                leverTransform.localEulerAngles = rotAxis * maxRot;
                if (Inventory.idDoors.Contains(idDoor))
                {
                    Inventory.idDoors.Remove(idDoor);
                }
            }
            //Open Door
            else
            {
               
                dir = 0;
                leverTransform.localEulerAngles = rotAxis * minRot;
                if (!Inventory.idDoors.Contains(idDoor))
                {
                    Inventory.idDoors.Add(idDoor);
                }
            }
        }
        public void DisableComponent(int enable)
        {
            if (enable == 0 && animatorBehaviour.CurrentAnimation > 0)
            {
                animatorBehaviour.Animator.enabled = false;
                if (!Inventory.idDoors.Contains(idDoor))
                {
                    Inventory.idDoors.Add(idDoor);
                }
            }

            if (enable == 1 && animatorBehaviour.CurrentAnimation < 1)
            {

                animatorBehaviour.Animator.enabled = true;
                if (Inventory.idDoors.Contains(idDoor))
                {
                    Inventory.idDoors.Remove(idDoor);
                }
            }
            
        }

    }
}
