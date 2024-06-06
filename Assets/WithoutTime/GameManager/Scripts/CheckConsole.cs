using UnityEngine;
#region Dll
using System.Runtime.InteropServices;
#endregion
namespace Dplds.Settings
{
    public class CheckConsole : MonoBehaviour
    {
        public enum TypeConsole { XbOne = 0, XbOneX = 1, XbSeriesS = 2, XbSeriesX = 3, Other = 4, XbSeriesSX = 5 }
        public static TypeConsole typeConsole;
        private int typeDevice;
        void Awake()
        {
            if (SystemInfo.deviceType == DeviceType.Console)
            {
                #region Dll
                typeDevice = IdentifyDevice();
                #endregion
            }
        }
        private void Start()
        {
            #region Dll
            typeConsole = (TypeConsole)typeDevice;
            #endregion
        }
        #region Dll
        [DllImport("__Internal")]
        static extern int IdentifyDevice();
        #endregion
    }
}