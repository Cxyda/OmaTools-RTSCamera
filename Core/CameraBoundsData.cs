using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTS_Camera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera bounds of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public class CameraBoundsData
    {
        [Tooltip("The collider which determines the area")]
        public BoxCollider CameraAreaCollider;
        [Range(0.1f, 10f)]
        [Tooltip("The force which pushed the camera back into the boundaries")]
        public float LimitationForce = 0.1f;
    }
}