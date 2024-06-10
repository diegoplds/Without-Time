using Dplds.Core;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class ButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private byte idButton = 0;
        [SerializeField] private Light pointLight;
        private AudioSource audioSource;
        private MeshRenderer meshRenderer;
        //private Animator animator;
        void Awake()
        {
           // animator = transform.parent.GetComponent<Animator>();
            meshRenderer = GetComponent<MeshRenderer>();
            audioSource = GetComponent<AudioSource>();
        }
        // Update is called once per frame
        void Update()
        {
            if (Inventory.idButton.Contains(idButton) && Inventory.idButton.Count > 0)
            {
                IsActive();
            }
            else
            {
                IsDisable();
            }
        }
       public void IsActive()
        {
            meshRenderer.material.SetColor("_EmissiveColor", Color.green);
            meshRenderer.material.SetColor("_BaseColor", Color.green);
            pointLight.color = Color.green;
            //animator.SetInteger("Movement", 1);//animation IsActive
        }
       public void IsDisable()
        {
            meshRenderer.material.SetColor("_EmissiveColor", Color.red);
            meshRenderer.material.SetColor("_BaseColor", Color.red);
            pointLight.color = Color.red;
           // animator.SetInteger("Movement", 0);//animation IsDisable
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CustomTag>())
            {
                var tag = other.gameObject.GetComponent<CustomTag>();
                if (tag.tags.Contains("Box"))
                {
                    if (!Inventory.idButton.Contains(idButton))
                    {
                        Inventory.idButton.Add(idButton);
                        if (!audioSource.isPlaying)
                            audioSource.PlayOneShot(clickClip);
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {

            if (other.GetComponent<CustomTag>())
            {
                var tag = other.gameObject.GetComponent<CustomTag>();
                if (tag.tags.Contains("Box"))
                {
                    if (Inventory.idButton.Contains(idButton))
                    {
                        Inventory.idButton.Remove(idButton);
                    }
                }
            }
        }
    }
}
