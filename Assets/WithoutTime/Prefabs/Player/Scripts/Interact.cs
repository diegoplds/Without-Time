using Dplds.Core;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class Interact : MonoBehaviour
    {
        [SerializeField] private float distanceInteract = 5.0f;
        [SerializeField] private LayerMask layerMask;
        public void CheckObjectInteractable(ref bool interact)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, distanceInteract, layerMask))
            {
                if (interact)
                {
                    if (_hit.collider != null)
                    {
                        var obj = _hit.collider.GetComponent<IInteractable>();
                        obj?.Interact();
                    }
                }
            }
        }
    }
}
