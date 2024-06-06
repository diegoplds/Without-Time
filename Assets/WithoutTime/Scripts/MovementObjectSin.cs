using UnityEngine;

namespace Dplds.General

{
    public class MovementObjectSin : MonoBehaviour
    {
        [SerializeField] private float amplitude = 0.5f;
        [SerializeField] private float speed = 1f;

        private float initialY; // Stores the initial position on the y-axis

        void Start()
        {
            // On Start, store the initial position
            initialY = transform.position.y;
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            float valueSen = Mathf.Sin(Time.time * speed);
            float sen = valueSen * amplitude;

            // Use the initial position to calculate the new position on the y-axis
            float newY = initialY + sen;

            // Set the new position of the object
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

    }
}
