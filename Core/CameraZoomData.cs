using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTS_Camera.Core
{
    /// <summary>
    /// TODO: 
    /// </summary>
    [Serializable]
    public struct CameraZoomData
    {
        public string ZoomAxisName;
        [Tooltip("The zoom speed for the camera")]
        public float ZoomSpeed;
        
        [Tooltip("The minimum camera field of view")]
        public float MinFOV;
        [Tooltip("The maximum camera field of view")]
        public float MaxFOV;

        public AnimationCurve CameraPitchingCurve;
        public float ZoomTime;
    }
}