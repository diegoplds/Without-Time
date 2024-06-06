using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
namespace Dplds.Gameplay
{
    public class DoorBase : MonoBehaviour
    {
        [SerializeField] protected int idDoor;
        [SerializeField] protected bool locked;
        [SerializeField] private MeshRenderer meshRendererLamp;
        [SerializeField] private HDAdditionalLightData lightData;
        [ColorUsage(true, true)]
        [SerializeField] private Color colorOpenDoor;
        [ColorUsage(true, true)]
        [SerializeField] private Color colorCloseDoor;
        protected void Update()
        {
            CheckHasKey();
        }
        protected void CheckHasKey()
        {
            if (Inventory.idDoors.Contains(idDoor))
            {
                if (meshRendererLamp != null)
                    meshRendererLamp.material.SetColor("_EmissiveColor", colorOpenDoor);
                if (lightData != null)
                    lightData.color = colorOpenDoor;
                locked = false;
            }
            else
            {
                if (meshRendererLamp != null)
                    meshRendererLamp.material.SetColor("_EmissiveColor", colorCloseDoor);
                if (lightData != null)
                    lightData.color = colorCloseDoor;
                locked = true;
            }
        }
    }
}
