using Dplds.Core;
using Dplds.Inputs;
using System;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class Player : MonoBehaviour, IDamageable
    {
        public static event Action OnDeath;
        #region Properties
        public CameraFps CameraFps { get => cameraFps; }
        public float Health => health;
        #endregion
        #region Fields Player
        [Header("Player")]
        [SerializeField] private float health = 100;
        [SerializeField] private Weapon weapon;
        private float currentHealth;
        #endregion
        #region Camera
        private CameraFps cameraFps;
        #endregion
        void Awake()
        {
            cameraFps = Camera.main.GetComponent<CameraFps>();
           
        }
        void Start()
        {
            
            #region Clear List
            Inventory.weapons.Clear();
            Inventory.idButton.Clear();
            Inventory.idDoors.Clear(); 
            #endregion
            currentHealth = health;
            CursorManagement.Instance.ShowCursor(false);
        }
        void Update()
        {
            Death();
        }
        public void TakeDamage(float damage)
        {
            if (currentHealth != 0)
            {
                currentHealth -= damage;
            }
        }
        public void Death()
        {
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                 OnDeath?.Invoke();
                SceneManagement.Instance.RestartLevel();
            }
        }
    }
}
