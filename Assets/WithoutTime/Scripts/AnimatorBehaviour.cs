using Dplds.Inputs;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class AnimatorBehaviour : MonoBehaviour
    {
        #region Properties
        public float CurrentAnimation { get => currentAnimation; }
        public bool CanPlayAnimation { get => canPlayAnimation; set => canPlayAnimation = value; }
        public int Dir { get => dir; set => dir = value; }
        public Animator Animator { get => animator;}
        #endregion
        #region Fields
        [SerializeField]private BoxCollider boxCollider;
        [SerializeField] private bool backAnimationWithWeapon;
        [Range(0, 1)]
        [SerializeField] private float initial;
        [Range(0.0f, 1)]
        [SerializeField] private float minAnimation = 0.0f;
        [Range(0.0f, 1)]
        [SerializeField] private float maxAnimation = 1f;
        private float currentAnimation;
        private Animator animator;
        private bool canPlayAnimation;
        private int dir = 0;
        #endregion
        void Awake()
        {
            animator = GetComponent<Animator>();
            //boxCollider = GetComponent<BoxCollider>();
        }
        private void Start()
        {
            //canPlayAnimation = true;
            animator.Play(animator.GetAnimatorTransitionInfo(0).fullPathHash, 0, initial);
        }
        void Update()
        {
            CheckCurrentAnimation();
        }
        void CheckCurrentAnimation()
        {
            currentAnimation = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            currentAnimation = Mathf.Clamp(currentAnimation, 0, 1);
            #region Animation With Weapon
            if (backAnimationWithWeapon)
            {
                //animation is over
                if (currentAnimation >= 1 && InputMaster.rewind < 0.5f)
                {
                    // animator.StopPlayback();
                    animator.SetFloat("Speed", 0);
                }
                //Start of animation
                else if (currentAnimation <= 0 && InputMaster.forward < 0.5f)
                {
                    //animator.StopPlayback();
                    animator.SetFloat("Speed", 0);
                }
                if (currentAnimation > 0)
                {
                    if (boxCollider != null)
                        boxCollider.enabled = true;
                }
                else
                {
                    if (boxCollider != null)
                        boxCollider.enabled = false;
                }
            }
            #endregion
            else
            {
                if (dir == 1)
                {
                    if (currentAnimation >= maxAnimation && canPlayAnimation)
                    {
                        animator.SetFloat("Speed", 0);
                        canPlayAnimation = false;
                    }
                }
                if (dir == 0)
                {
                    if (currentAnimation <= minAnimation && canPlayAnimation)
                    {
                        animator.SetFloat("Speed", 0);
                        canPlayAnimation = false;
                    }
                }
            }
        }
    }
}
