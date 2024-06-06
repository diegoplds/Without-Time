using Dplds.Core;
using Dplds.Storage;
using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
namespace Dplds.Settings
{
    public class CameraSettings : MonoBehaviour
    {
        private Camera _camera;
        private HDAdditionalCameraData hdAdditionalCameraData;
        void Awake()
        {
            _camera = GetComponent<Camera>();
            hdAdditionalCameraData = GetComponent<HDAdditionalCameraData>();
        }
        private void Start()
        {
            CameraGraphics();
            VideoSettings.OnChangeValue += CameraGraphics;
        }
        void CameraGraphics()
        {
            //_camera.allowHDR = Convert.ToBoolean( PlayerPrefs.GetInt(NamePrefs.HDR + GameManagement.key));
            // _camera.renderingPath = (RenderingPath)PlayerPrefs.GetInt("Rendering Path"+GameManager.key);
            hdAdditionalCameraData.antialiasing = (HDAdditionalCameraData.AntialiasingMode)PlayerPrefs.GetInt(NamePrefs.ANTIALIASING + GameManagement.key);
        }
        private void OnDestroy()
        {
            VideoSettings.OnChangeValue -= CameraGraphics;
        }
    }
}
