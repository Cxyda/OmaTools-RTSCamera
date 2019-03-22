using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera keyboard controls of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct CameraMovementKeyboardControlData
    {
        [Range(0.1f, 10f)]
        public float Acceleration;
        [Range(0.01f, 1f)]
        public float Damping;
        [Range(0.1f, 10f)]
        public float MaxCameraSpeed;
        [Space]

        public string HorizontalPanAxisName;
        public string VerticalPanAxisName;
    }
}