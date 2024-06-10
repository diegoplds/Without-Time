using UnityEngine;

namespace Dplds.Gameplay
{
    public class RotatePlatform : MonoBehaviour
    {
        [SerializeField] private Vector3 axisRot = new Vector3(0, 1, 0);
        [SerializeField] private int incrementRot = 25;
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RotateObj();
        }
        void RotateObj()
        {
            transform.Rotate(axisRot * incrementRot);
        }
    }
}
