using Dplds.Core;
using Dplds.Inputs;
using UnityEngine;
namespace Dplds.Gameplay
{

    public class Movement : MonoBehaviour, IMovement
    {
        public enum StatePlayer { idle, walking, running }
        #region Properties
        public AudioSource AudioSource { get => audioSource; }
        public AudioClip[] StepsClips => stepsClips;
        public float SpeedWalk => speedWalk;
        public float SpeedRun => speedRun;
        public int TypeGround { get => typeGround; set => typeGround = value; }
        #endregion
        #region Fields
        [SerializeField] private AudioClip[] stepsClips;
        [SerializeField] private float force = 35.0f;
        [SerializeField] private float stepOffsetDefault = 0.45f;
        [SerializeField] private float jumpHeight = 15.0f;
        [SerializeField] private float gravityValue = -9.8f;
        [SerializeField] private float timeToJump = 0.5f;
        [SerializeField] private float speedWalk = 35.0f;
        [SerializeField] private float speedRun = 75.0f;
        #endregion
        private CheckIsGround checkGround;
        private AudioSource audioSource;
        private CharacterController characterController;
        private Animator animator;
        private StatePlayer statePlayer;
        private Vector3 playerVelocity;
        private bool isGround;
        private bool canJump;
        private bool running;
        private float timeCountjump;
        private int typeGround;

        // Start is called before the first frame update
        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            checkGround = GetComponentInChildren<CheckIsGround>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>(); 
        }
        void Start()
        {
         /*   if (Inventory.weapons.Contains())
            {

            }*/
            Item.OnGetItem += SetActiveWeaponAnimation;
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.GetComponent<Rigidbody>())
            {
                if (characterController.collisionFlags != CollisionFlags.Below
                || characterController.collisionFlags != CollisionFlags.Above)
                {
                    hit.gameObject.GetComponent<Rigidbody>().AddForce(force * InputMaster.move.y * Time.deltaTime * transform.forward
                        + force * InputMaster.move.x * Time.deltaTime * transform.right
                        , ForceMode.Impulse);
                }
            }
        }
        public void HandleMovement(ref Vector2 move, ref float running, ref bool jump)
        {
            if (!canJump)
            {
                timeCountjump += Time.deltaTime;
                if (timeCountjump > timeToJump)
                    canJump = true;
            }
            IsGround(ref move);
            #region Walk & Run
            if (move.x < -0.5f || move.y < -0.5f || move.x > 0.5f || move.y > 0.5f)
            {
                if (isGround)
                {
                    if (!this.running)
                        statePlayer = StatePlayer.walking;
                }
            }
            else
            {
                this.running = false;
                statePlayer = StatePlayer.idle;
            }
            //when the run button is pressed it checks if the player is able to run
            if (running >= 1 && isGround)
            {
                if (move.y >= 0.5f)
                {
                    statePlayer = StatePlayer.running;
                    this.running = true;
                    //weapons.shooting = false;
                }
            }
            if (move.y <= 0)
            {
                this.running = false;
            }
            SpeedPlayer(ref move);
            #endregion
            #region JUMP
            if (jump && isGround && canJump)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                characterController.stepOffset = 0.01f;
                canJump = false;
                timeCountjump = 0;
            }
            #endregion
            StateAnimation();
        }
        void SpeedPlayer(ref Vector2 move)
        {
            if (move.x > 0.5f || move.x < -0.5 || move.y > 0.5f || move.y < -0.5f)
            {
                if (statePlayer == StatePlayer.walking)
                {
                    characterController.Move((move.y * speedWalk * transform.forward + move.x * speedWalk * transform.right) * Time.deltaTime);
                }
                else if (running)
                {
                    characterController.Move((move.y * speedRun * transform.forward + move.x * speedRun * transform.right) * Time.deltaTime);
                }
            }
        }
        void IsGround(ref Vector2 move)
        {
            isGround = checkGround.IsGround;
            if (isGround && playerVelocity.y <= 0)
            {
                characterController.stepOffset = stepOffsetDefault;
                playerVelocity.y = 0;
                //se o jogador estiver correndo quando pulou e toca o solo volta para animação correr
                if (move.x > 0.5f || move.x < -0.5f || move.y > 0.5f || move.y < -0.5f)
                {
                    if (running)
                    {
                        statePlayer = StatePlayer.running;
                    }
                }
            }
            else
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
                characterController.Move(playerVelocity * Time.deltaTime);
                if (this.running)
                {
                    statePlayer = StatePlayer.idle;
                }
            }
        }
        void StateAnimation()
        {
            switch (statePlayer)
            {
                case StatePlayer.idle:
                    animator.SetInteger("moving", 0);
                    break;
                case StatePlayer.walking:
                    animator.SetInteger("moving", 1);
                    break;
                case StatePlayer.running:
                    animator.SetInteger("moving", 2);
                    break;
            }

        }
        void SetActiveWeaponAnimation()
        {
            animator.SetTrigger("Draw");
        }
        public void PlaySoundStep()
        {
            if (isGround)
            {
                float pitch = Random.Range(0.7f, 1);
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(stepsClips[typeGround]);
            }
        }
        void OnDestroy()
        {
            Item.OnGetItem -= SetActiveWeaponAnimation;
        }
    }
}
