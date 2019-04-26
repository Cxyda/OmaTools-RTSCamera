using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides general movement data for the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct CameraMovementData
    {
        [Range(0.1f, 20f)]
        [Tooltip("The camera acceleration after a move command was received")]
        public float Acceleration;
        [Range(0.01f, 1f)]
        [Tooltip("The movement damping after no movement command was received")]
        public float Damping;
        [Range(0.1f, 10f)]
        [Tooltip("The maximum movement speed of the camera")]
        public float MaxCameraSpeed;
    }
}