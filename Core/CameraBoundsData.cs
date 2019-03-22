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
        public BoxCollider CameraAreaCollider;
        [Range(0.1f, 10f)]
        public float LimitationForce = 0.1f;
    }
}