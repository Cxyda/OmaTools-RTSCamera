using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera bounds of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public class CameraBoundsData
    {
        [Tooltip("The collider which determines the area")]
        public BoxCollider CameraAreaCollider;
        [Range(0.1f, 1f)]
        [Tooltip("The force which pushed the camera back into the boundaries")]
        public float PushbackStrength = 0.4f;
    }
}