using UnityEngine;

namespace Dplds.Gameplay
{
    public class ImpactSound : MonoBehaviour
    {
        [SerializeField] private AudioClip imapactSoundFx;
       [SerializeField] private AudioSource audioSource;
        [SerializeField] private int dirAnimation;
        [SerializeField] private float minPitch = 1;
        [SerializeField] private float maxPitch = 1.7f;
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        public void PlaySoundImpact()
        {
            if (animator.GetFloat("Speed") > dirAnimation)
            {
                var pitch = Random.Range(minPitch, maxPitch);
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(imapactSoundFx);
            }
        }
    }
}
