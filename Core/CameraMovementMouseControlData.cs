using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// TODO: 
    /// </summary>
    [Serializable]
    public struct CameraMovementMouseControlData
    {
        [Range(0.1f, 10f)]
        public float Acceleration;
        [Range(0.001f, 1f)]
        public float Damping;
        [Range(0.1f, 10f)]
        public float MaxCameraSpeed;
        [Space]
        public int TopScrollPadding;
        public int LeftScrollPadding;
        public int BottomScrollPadding;
        public int RightScrollPadding;

    }
}