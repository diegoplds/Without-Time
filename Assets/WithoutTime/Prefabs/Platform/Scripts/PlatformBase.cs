using Dplds.Core;
using UnityEngine;
namespace Dplds.Gameplay
{
    public class PlatformBase : MonoBehaviour
    {
        #region Properties
        public bool CanMove { get => canMove; set=>canMove = value; }
        public float TimeToMove { get => timeToMove;}
        #endregion
        #region Fields
        [SerializeField] private Vector3 axisDir = new(1, 0, 0);
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float speed = 10;
        [SerializeField] private float timeToMove = 4;
        [Tooltip("use a number from 0 to 1 to say which direction you are going 0 to the negative and 1 to the positive")]
        [Range(0f, 1f)]
        [SerializeField] private int dir;
        [SerializeField] private BoxCollider[] boxColliders;
        private bool canMove;
        private float time; 
        #endregion
        protected void Move()
        {
            if (canMove)
            {
                //Platform direction on x-axis
                #region X-Axis
                if (axisDir.x >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    else
                        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                    if (transform.position.x >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
                        dir = 0;
                    }
                    else if (transform.position.x <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(minDistance, transform.position.y, transform.position.z);
                        dir = 1;
                    }
                }
                #endregion
                //Y-Axis
                #region Y-Axis
                if (axisDir.y >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                    else
                        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                    if (transform.position.y >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, maxDistance, transform.position.z);
                        dir = 0;
                    }
                    else if (transform.position.y <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, minDistance, transform.position.z);
                        dir = 1;
                    }
                }
                #endregion
                //Z-Axis
                #region Z-Axis
                if (axisDir.z >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                    else
                        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                    if (transform.position.z >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, transform.position.y, maxDistance);
                        dir = 0;
                    }
                    else if (transform.position.z <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, transform.position.y, minDistance);
                        dir = 1;
                    }
                }
                #endregion
            }
           
        }
        protected void Move(int id)
        {
            if (canMove)
            {
                //Platform direction on x-axis
                #region X-Axis
                if (axisDir.x >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                    else
                        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                    if (transform.position.x >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
                        //Inventory.idButton.Remove(id);
                        dir = 0;
                    }
                    else if (transform.position.x <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(minDistance, transform.position.y, transform.position.z);
                        //Inventory.idButton.Remove(id);
                        dir = 1;
                    }
                }
                #endregion
                //Y-Axis
                #region Y-Axis
                if (axisDir.y >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                    else
                        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                    if (transform.position.y >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, maxDistance, transform.position.z);
                        Inventory.idButton.Remove(id);
                        dir = 0;
                    }
                    else if (transform.position.y <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, minDistance, transform.position.z);
                        Inventory.idButton.Remove(id);
                        dir = 1;
                    }
                }
                #endregion
                //Z-Axis
                #region Z-Axis
                if (axisDir.z >= 1)
                {
                    if (dir == 1)
                        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                    else
                        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                    if (transform.position.z >= maxDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, transform.position.y, maxDistance);
                        Inventory.idButton.Remove(id);
                        dir = 0;
                    }
                    else if (transform.position.z <= minDistance)
                    {
                        canMove = false;
                        transform.position = new Vector3(transform.position.x, transform.position.y, minDistance);
                        Inventory.idButton.Remove(id);
                        dir = 1;
                    }
                }
                #endregion
            }
        }
       public void CanMoveMethod()
        {
            if (!CanMove)
            {
                time += Time.deltaTime;
                if (time > TimeToMove)
                {
                    time = 0;
                    CanMove = true;
                }
            }
        }
    }
}
