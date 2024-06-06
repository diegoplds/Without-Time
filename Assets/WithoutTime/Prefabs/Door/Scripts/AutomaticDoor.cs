using UnityEngine;
namespace Dplds.Gameplay
{
    public class AutomaticDoor : DoorBase
    {
        [SerializeField] private bool openDoorWithTrigger;
        [SerializeField] private float openPitch = 1.5f;
        [SerializeField] private float lockedPitch = 1.0f;
        [SerializeField] private AudioClip openFx;
        [SerializeField] private AudioClip lockFx;
        [SerializeField] private AudioSource audioSource;
        private Animator animator;
        private AnimatorBehaviour animatorBehaviour;
        void Awake()
        {
            animator = GetComponent<Animator>();
            animatorBehaviour = GetComponent<AnimatorBehaviour>();
        }
        private new void Update()
        {
            base.Update();
            if (!openDoorWithTrigger)
            {
                if (Inventory.idDoors.Contains(idDoor))
                    DoorBehaviour();
                else
                {
                    animatorBehaviour.CanPlayAnimation = true;
                    animatorBehaviour.Dir = 0;
                    animator.SetFloat("Speed", -1);//Close
                }
            }
        }
        void DoorBehaviour()
        {
            if (!locked)//Unlock
            {
                animatorBehaviour.CanPlayAnimation = true;
                animatorBehaviour.Dir = 1;
                audioSource.pitch = openPitch;
                if (openDoorWithTrigger)
                    audioSource.PlayOneShot(openFx);
                animator.SetFloat("Speed", 1);//Open
            }
            else//Lock
            {
                audioSource.pitch = lockedPitch;
                audioSource.PlayOneShot(lockFx);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if ((openDoorWithTrigger))
            {
                if (other.CompareTag("Player"))
                {
                    DoorBehaviour();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if ((openDoorWithTrigger))
            {
                if (!locked)//Unlock
                {
                    if (other.CompareTag("Player"))
                    {
                        animatorBehaviour.CanPlayAnimation = true;
                        animatorBehaviour.Dir = 0;
                        animator.SetFloat("Speed", -1);//Close
                    }
                }
            }
        }
        public void PlaySoundDoor()
        {
            if (!locked)
            {
                audioSource.pitch = openPitch;
                audioSource.PlayOneShot(openFx);
            }
            else
            {
                audioSource.pitch = lockedPitch;
                audioSource.PlayOneShot(lockFx);
            }
        }
    }
}