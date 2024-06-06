using Dplds.Core;
using Dplds.Inputs;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class Player : MonoBehaviour, IDamageable
    {
        #region Properties
        public CameraFps CameraFps { get => cameraFps; }
        public float Health => health;
        #endregion
        #region Fields Player
        [Header("Player")]
        [SerializeField] private float health = 100;
        private float currentHealth;
        private int typeGround;
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
                SceneManagement.Instance.RestartLevel();
            }
        }
    }
}
