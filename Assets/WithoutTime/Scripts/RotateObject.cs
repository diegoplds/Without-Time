using System;
using UnityEngine;

namespace Dplds.General
{
    [Serializable]
    public struct MinMaxRot
    {
        [Header("Min & Max Rotation")]
        [SerializeField] public float minX;
        [SerializeField] public float maxX;
        //
        [SerializeField] public float minY;
        [SerializeField] public float maxY;
        //
        [SerializeField] public float minZ;
        [SerializeField] public float maxZ;
    }

    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private bool clampRot  = true;
        [SerializeField] private Vector3 rotDir = new Vector3(1, 1, 0);
        [SerializeField] private float speedRot = 5f;
        [SerializeField] private float divide = 150;
        [SerializeField] private MinMaxRot minMaxRot;
       void Update()
        {
            if (!clampRot) { return; }

            var eulerAngle = new Vector3(
                Mathf.Clamp(transform.rotation.eulerAngles.x, minMaxRot.minX, minMaxRot.maxX),
                Mathf.Clamp(transform.rotation.eulerAngles.y, minMaxRot.minY, minMaxRot.maxY),
                Mathf.Clamp(transform.rotation.eulerAngles.z, minMaxRot.minZ, minMaxRot.maxZ)
            );
            transform.localEulerAngles = eulerAngle;
        }

        private void FixedUpdate()
        {
            transform.Rotate((rotDir * speedRot / divide));
        }
    }
}
