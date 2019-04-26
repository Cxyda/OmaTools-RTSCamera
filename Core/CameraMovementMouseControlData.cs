using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera mouse controls of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct CameraMovementMouseControlData
    {
        [Tooltip("The top-border with in pixels")]
        public int TopScrollPadding;
        [Tooltip("The left-border with in pixels")]
        public int LeftScrollPadding;
        [Tooltip("The bottom-border with in pixels")]
        public int BottomScrollPadding;
        [Tooltip("The right-border with in pixels")]
        public int RightScrollPadding;

    }
}