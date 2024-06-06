using Dplds.Core;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class CheckTypeGround : MonoBehaviour
    {
        private Movement player;
        private void Awake()
        {
            player = transform.parent.GetComponent<Movement>();
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<CustomTag>())
            {
                CustomTag tags = other.GetComponent<CustomTag>();
                if (tags.tags.Contains("Concrete"))
                {
                    player.TypeGround = 0;
                }
                if (tags.tags.Contains("Metal"))
                {
                    player.TypeGround = 1;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CustomTag>())
            {
                CustomTag tags = other.GetComponent<CustomTag>();
                IMovement obj = player;
                if (!player.AudioSource.isPlaying)
                {
                    if (tags.tags.Contains("Concrete"))
                    {
                        float pitch = Random.Range(0.7f, 1);
                        player.AudioSource.pitch = pitch;
                        player.AudioSource.PlayOneShot(obj.StepsClips[0]);
                    }
                    if (tags.tags.Contains("Metal"))
                    {
                        float pitch = Random.Range(0.7f, 1);
                        player.AudioSource.pitch = pitch;
                        player.AudioSource.PlayOneShot(obj.StepsClips[1]);
                    }
                }
            }
        }
    }
}
