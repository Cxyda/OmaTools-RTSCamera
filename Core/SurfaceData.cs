using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTS_Camera.Core
{
    /// <summary>
    /// This class provides all customizable properties for the floating feature <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct SurfaceData
    {
        [Tooltip("The layer of the GameObject where the RTS camera should hover over")]
        public LayerMask SurfaceMask;
        [Tooltip("The height of the camera which should be maintained")]
        public float FloatingHeight;
    }
}