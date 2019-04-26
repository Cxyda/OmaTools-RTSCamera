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

        [Header("Camera Target Settings")] 
        public float TransitionTime = 5f;
        
        [Header("Camera Floating")]
        public bool EnableSurfaceFloating;
        public SurfaceData SurfaceData;

        [Header("Camera Movement")] 
        public CameraMovementData CameraMovementData;
        
        public bool EnableKeyboardMovement;
        public CameraMovementKeyboardControlData KeyboardData;
        
        public bool EnableMouseMovement;
        public CameraMovementMouseControlData MouseData;

        [Header("Camera Zooming")] 
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

            CameraMovementData.Acceleration = 5f;
            CameraMovementData.Damping = 0.15f;
            CameraMovementData.MaxCameraSpeed = 2f;
            
            KeyboardData.HorizontalPanAxisName = "Horizontal";
            KeyboardData.VerticalPanAxisName = "Vertical";

            MouseData.BottomScrollPadding = MouseData.TopScrollPadding = Screen.height / 10;
            MouseData.LeftScrollPadding = MouseData.RightScrollPadding= Screen.width / 10;

            CameraZoomData.ZoomAxisName = "Mouse ScrollWheel";
            CameraZoomData.ZoomTime = 1f;
            CameraZoomData.ZoomSpeed = 50f;
            CameraZoomData.MinFOV = 30;
            CameraZoomData.MaxFOV = 60;
            
            PivotOffset = 50;
            RotationControlData.RotationTrigger = MouseButton.RightMouseButton;
            RotationControlData.RotationSensitivity = 0.2f;
        }
    }
}