using Dplds.Core;
using UnityEngine;

namespace Dplds.Gameplay
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] private float takeDamage = 10.0f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var obj = other.GetComponent<IDamageable>();
                obj.TakeDamage(takeDamage);
            }
        }
    }
}
