using Dplds.Core;
using UnityEngine;

namespace Dplds.Gameplay
{
    public class Weapon : MonoBehaviour
    {
        public bool CanShoot { get => canShoot; set => canShoot = value; }
        #region Fields
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
        #endregion

       
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
     public   void CheckHasWeapon()
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
        public void BackTime(ref float rewind,ref float forward,ref float stop)
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
#if UNITY_EDITOR
                            Debug.Log("Rewind");
#endif
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
#if UNITY_EDITOR
                            Debug.Log("Forward");
#endif
                        }
                    }
                    else if (stop > 0.1f)
                    {
                        animator.SetFloat("Speed", 0);
#if UNITY_EDITOR
                        Debug.Log("Stop");
#endif
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
        private void OnLevelWasLoaded(int level)
        {
            CheckHasWeapon();
        }
        private void OnDestroy()
        {
            Item.OnGetItem -= CheckHasWeapon;
           // Player.OnDeath -= CheckHasWeapon;
        }
    }
}
