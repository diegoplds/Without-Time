using UnityEngine;

namespace Dplds.Gameplay
{
    public class FollowObject : MonoBehaviour
    {
        [SerializeField] private bool LerpPos;
        [SerializeField] private float speedRotLerp = 4.0f;
        [SerializeField] private float speedPosLerp = 4.0f;
        [SerializeField] private Transform target;
        void Update()
        {
            if(LerpPos)
                FollowObjectLerp();
            RotationObjectLerp();
        }
        private void LateUpdate()
        {
            if (!LerpPos)
                MethodFollowObject();
        }
       void FollowObjectLerp()
        {
            var speed = speedPosLerp * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
        }
        void MethodFollowObject()
        {
            transform.localPosition = target.position;
        }
        void RotationObjectLerp()
        {
            var speed = speedRotLerp * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation,target.rotation, speed ) ;
        }
    }
}
