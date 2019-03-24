using System;

namespace Plugins.O.M.A.Games.RTS_Camera.Core
{
    /// <summary>
    /// This class provides all customizable properties for camera keyboard controls of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [Serializable]
    public struct CameraMovementKeyboardControlData
    {
        public string HorizontalPanAxisName;
        public string VerticalPanAxisName;
    }
}