using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera mouse controls of the <see cref="RTSCameraComponent"/>
    /// </summary>
    public enum MouseButton
    {
        LeftMouseButton = 0,
        RightMouseButton = 1,
        MiddleMouseButton = 2
    }
    [Serializable]
    public struct CameraRotationMouseControlData
    {
        public MouseButton RotationTrigger;
        public float RotationSensitivity;
        public bool InvertRotationAxis;
        
        [NonSerialized] public Vector3 MouseDownPosition;
    }
}