using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTS_Camera.Core
{
    /// <summary>
    /// This class provides initial transformations for the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct CameraInitialTransformData
    {
        public Vector3 InitialPosition;
        public Vector2 InitialRotation;
    }
}