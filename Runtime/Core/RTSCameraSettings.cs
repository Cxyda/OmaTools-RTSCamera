using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Core
{
    [CreateAssetMenu(menuName = "O.M.A. Tool/RTS Camera/Settings", fileName = "RTSCameraSettings")]
    public class RtsCameraSettings : ScriptableObject
    {
        [Header("Movement")]
        [Tooltip("The maximum movement speed of the camera. This is the speed the camera will move when the input axis is at its maximum value.)")]
        public float MaxMovementSpeed = 15.0f;
        [Range(0.1f, 10f)]
        [Tooltip("The acceleration of the camera when moving.")]
        public float MovementAcceleration = 2.5f;

        [Header("Zoom")]
        [Tooltip("The the sizes of the camera when zooming in and out.")]
        public float ZoomStepSize = 5f;
        [Tooltip("The speed at which the camera zooms in and out.")]
        public float ZoomSpeed = 10.0f;
        [Tooltip("The minimum distance the camera can zoom in and out.")]
        public float MinZoomDistance = 10.0f;
        [Tooltip("The maximum distance the camera can zoom in and out.")]
        public float MaxZoomDistance = 50.0f;

        [Header("Look")]
        [Tooltip("The speed at which the camera rotates.")]
        public float LookTurnSpeed = 2.0f;
        [Tooltip("The damping of the camera rotation.")]
        public float LookTurnSpeedDamping = 0.05f;
        [Tooltip("The step angles at which the camera rotates when using the free rotation.")]
        public float StepTurnAngle = 45.0f;
        [Tooltip("The speed at which the camera rotates when using the step rotation.")]
        public float StepTurnSpeed = 5.0f;
        [Tooltip("The minimum pitch angle of the camera.")]
        public float MinPitchAngle = 20f;
        [Tooltip("The maximum pitch angle of the camera.")]
        public float MaxPitchAngle = 90f;
        [Tooltip("The angle at which the camera snaps to the next turn step when coming close to that value.")]
        public float RotationSnapAngle = 0.5f;
        [Tooltip("The threshold at which the camera snaps to the next turn step angle instead of the closest step angle.")]
        public float NextTurnStepAngleThreshold = 10f;

        [Header("Optional Settings")]
        [Tooltip("The layer mask of the terrain. This is used to make the camera follow the terrain.")]
        public LayerMask TerrainLayer;
        [Tooltip("The speed at which the camera follows the terrain.")]
        public float TerrainFollowSpeed = 5f;
        [Tooltip("The distance at which the camera raycasts the terrain.")]
        public float TerrainRaycastDistance = 10000f;
        [Range(1f, 100f)]
        [Tooltip("The force at which the camera is pushed back when it is outside the bounds.")]
        public float BoundsPushbackForce = 8f;

        [Header("Camera System Settings")]
        [Tooltip("The tag of the camera target.")]
        public string CameraTargetTagName = "CameraTarget";

        [Tooltip("Can the camera be moved by moving the cursor close the the edges of the screen?")]
        public bool AllowScreenScroll = true;
        [Tooltip("The area at the top of the screen where the camera can be moved by moving the cursor.")]
        public float TopScrollArea = 0.85f;
        [Tooltip("The area at the bottom of the screen where the camera can be moved by moving the cursor.")]
        public float BottomScrollArea = 0.15f;
        [Tooltip("The area at the left of the screen where the camera can be moved by moving the cursor.")]
        public float LeftScrollArea = 0.1f;
        [Tooltip("The area at the right of the screen where the camera can be moved by moving the cursor.")]
        public float RightScrollArea = 0.9f;
    }
}