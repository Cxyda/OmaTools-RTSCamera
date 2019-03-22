using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all customizable properties for the floating feature <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct SurfaceData
    {
        public LayerMask SurfaceMask;
        public float FloatingHeight;
    }
}