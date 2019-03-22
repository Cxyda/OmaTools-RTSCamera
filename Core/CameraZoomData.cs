using System;
using UnityEngine;
using UnityEngine.Animations;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// TODO: 
    /// </summary>
    [Serializable]
    public struct CameraZoomData
    {
        public string ZoomAxisName;

        public float ZoomSpeed;
        
        public float MinFOV;
        public float MaxFOV;

        public AnimationCurve CameraPitchingCurve;
        public float ZoomTime;
    }
}