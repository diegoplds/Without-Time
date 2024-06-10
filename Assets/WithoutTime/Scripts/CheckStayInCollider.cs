using UnityEngine;

namespace Dplds.Gameplay
{
    public class CheckStayInCollider : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _weapon.CanShoot = false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _weapon.CanShoot = true;
            }
        }
    }
}
