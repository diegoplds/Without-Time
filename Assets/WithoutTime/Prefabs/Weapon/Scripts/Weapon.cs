using Dplds.Core;
using UnityEngine;

namespace Dplds.Gameplay
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private int idWeapon;
        [SerializeField] private float distanceCheckObj = 15.0f;
        [SerializeField] private Transform posRay;
        [SerializeField] private Texture[] texturesEmissive = new Texture[2];
        private MeshRenderer meshRenderer;
        private Transform _camera;
        private RaycastHit _hit;
        private Animator animator;
        private AnimatorBehaviour animatorBehaviour;
        private bool canShoot;
        void Awake()
        {
            _camera = Camera.main.transform;
            meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
        }
        private void Start()
        {
            CheckHasWeapon();
            Item.OnGetItem += CheckHasWeapon;
        }
        void CheckHasWeapon()
        {
            if (!Inventory.weapons.Contains(idWeapon))
            {
                canShoot = false;
                meshRenderer.enabled = false;
            }
            else
            {
                canShoot = true;
                meshRenderer.enabled = true;
            }
        }
        public void BackTime(ref float rewind,ref float forward)
        {
            if (canShoot)
            {
                if (CheckObject())
                {
                    if (animator == null)
                    {
                        animator = _hit.transform.parent.GetComponent<Animator>();
                        animatorBehaviour = animator.GetComponent<AnimatorBehaviour>();
                    }
                    if (rewind > 0.5f)
                    {
                        if (animatorBehaviour.CurrentAnimation > 0)
                        {
                            if(animator.enabled == false)
                                animator.enabled = true;
                            int animationReverse = -1;
                            animator.SetFloat("Speed", animationReverse);
                            Debug.Log("Rewind");
                        }

                    }
                    else if (forward > 0.5f)
                    {
                        if (animatorBehaviour.CurrentAnimation < 1)
                        {
                            if (animator.enabled == false)
                                animator.enabled = true;
                            int animationReverse = 1;
                            animator.SetFloat("Speed", animationReverse);
                            Debug.Log("Forward");
                        }
                    }
                }
            }
        }
       
        bool CheckObject()
        {
            bool check = false;
            if (Physics.Raycast(posRay.position, _camera.transform.forward, out _hit, distanceCheckObj,layerMask))
            {
                Debug.DrawLine(posRay.position, _hit.point);
                if (_hit.transform.gameObject.GetComponent<CustomTag>())
                {
                    var tag = _hit.transform.gameObject.GetComponent<CustomTag>();
                    if(tag.tags.Contains("Animation Back Time"))
                    {
                        meshRenderer.material.SetTexture("_EmissiveColorMap", texturesEmissive[1]);

                        check = true;
                    }
                }
                else
                {
                   
                }
            }
            else
            {
                if (animator != null)
                {
                    animator = null;
                    animatorBehaviour = null;
                }
                meshRenderer.material.SetTexture("_EmissiveColorMap", texturesEmissive[0]);
            }
            return check;
        }
        private void OnDestroy()
        {
            Item.OnGetItem -= CheckHasWeapon;
        }
    }
}
