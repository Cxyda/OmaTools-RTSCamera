using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Core
{
    /// <summary>
    /// This class provides all settings of the <see cref="RTSCameraComponent"/>
    /// </summary>
    [CreateAssetMenu (menuName = "O.M.A.Tools/Camera/RTS Camera Settings", fileName = "RTSCameraSettings")]
    public class RTSCameraSettings : ScriptableObject
    {
        [Header("Initial Camera Transform")]
        public CameraInitialTransformData InitialTransformData;
        
        [Header("Camera Floating")]
        public bool EnableSurfaceFloating;
        public SurfaceData SurfaceData;
        
        [Header("Camera Movement")]
        public bool EnableKeyboardMovement;
        public CameraMovementKeyboardControlData KeyboardData;
        
        public bool EnableMouseMovement;
        public CameraMovementMouseControlData MouseData;

        public bool EnableCameraZoom;
        public CameraZoomData CameraZoomData;
        
        [Header("Camera Rotation")]
        public float PivotOffset;

        public bool EnableMouseRotation;
        public CameraRotationMouseControlData RotationControlData;

        private void Awake()
        {
            InitialTransformData.InitialRotation = new Vector3(40,0,0);
            SurfaceData.FloatingHeight = 20;

            KeyboardData.Acceleration = 2f;
            KeyboardData.Damping = 0.2f;
            KeyboardData.MaxCameraSpeed = 1.8f;
            KeyboardData.HorizontalPanAxisName = "Horizontal";
            KeyboardData.VerticalPanAxisName = "Vertical";

            MouseData.BottomScrollPadding = MouseData.TopScrollPadding = Screen.height / 20;
            MouseData.LeftScrollPadding = MouseData.RightScrollPadding= Screen.width / 20;
            MouseData.Acceleration = 5f;
            MouseData.Damping = 0.2f;
            MouseData.MaxCameraSpeed = 2f;

            CameraZoomData.ZoomAxisName = "Mouse ScrollWheel";
            CameraZoomData.ZoomTime = 1f;
            CameraZoomData.ZoomSpeed = 5f;
            CameraZoomData.MinFOV = 30;
            CameraZoomData.MaxFOV = 60;
            
            PivotOffset = 50;
            RotationControlData.RotationTrigger = MouseButton.RightMouseButton;
            RotationControlData.RotationSensitivity = 0.2f;
        }
    }
}