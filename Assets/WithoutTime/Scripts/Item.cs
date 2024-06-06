using Dplds.Core;
using System;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class Item : MonoBehaviour, IInteractable
    {
        public static event Action OnGetItem;
        public enum TypeItem { Weapon, Key }
        [SerializeField] private TypeItem typeItem;
        [SerializeField] private int idItem;
        private MeshRenderer meshRenderer;
        private SphereCollider sphereCollider;
        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            sphereCollider = GetComponent<SphereCollider>();
        }
        void Start()
        {
            OnGetItem += CheckhasItem;
            CheckhasItem();
        }
        void CheckhasItem()
        {
            #region Weapon
            if (typeItem == TypeItem.Weapon)
            {
                if (Inventory.weapons.Contains(idItem))
                {
                    meshRenderer.enabled = false;
                    sphereCollider.enabled = false;
                }
                else
                {
                    meshRenderer.enabled = true;
                    sphereCollider.enabled = true;
                }
            }
            #endregion
            #region Key
            if (typeItem == TypeItem.Key)
            {
                if (Inventory.idDoors.Contains(idItem))
                {
                    meshRenderer.enabled = false;
                    sphereCollider.enabled = false;
                }
                else
                {
                    meshRenderer.enabled = true;
                    sphereCollider.enabled = true;
                }
            }
            #endregion
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (typeItem == TypeItem.Weapon)
                {
                    if (!Inventory.weapons.Contains(idItem))
                    {
                        Inventory.weapons.Add(idItem);
                        OnGetItem?.Invoke();
                    }
                }
                if (typeItem == TypeItem.Key)
                {
                    if (!Inventory.idDoors.Contains(idItem))
                    {
                        Inventory.idDoors.Add(idItem);
                        OnGetItem?.Invoke();
                    }
                }
            }
        }
        public void Interact()
        {
            if (typeItem == TypeItem.Weapon)
            {
                if (!Inventory.weapons.Contains(idItem))
                {
                    Inventory.weapons.Add(idItem);
                    OnGetItem?.Invoke();
                }
            }
            if (typeItem == TypeItem.Key)
            {
                if (!Inventory.idDoors.Contains(idItem))
                {
                    Inventory.idDoors.Add(idItem);
                    OnGetItem?.Invoke();
                }
            }
        }
        void OnDestroy()
        {
            OnGetItem -= CheckhasItem;
        }
    }
}
