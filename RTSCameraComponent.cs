using Plugins.O.M.A.Games.Core.ErrorHandling;
using Plugins.O.M.A.Games.RTS_Camera.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Plugins.O.M.A.Games.RTS_Camera
{
    /// <summary>
    /// The <see cref="RTSCameraComponent"/> is a Unity3D component to easily control a floating camera over a given
    /// LayerMask. In addition it provides the following features.
    ///     - Floating in a certain distance over a given LayerMask
    ///     - Camera movement controls via keyboard
    ///     - Camera movement controls via mouse (screen borders)
    ///     - Camera zooming
    ///     - Camera rotation controls via Mouse
    ///     - Camera movement limitation via BoundingBox
    /// </summary>
    public class RTSCameraComponent : MonoBehaviour
    {
        public Transform CameraPivot;

        public RTSCameraSettings Settings;

        [Header("Camera Bounds")]
        public bool EnableCameraBounds;
        public CameraBoundsData CameraLimitation = new CameraBoundsData();

        private Camera _camera;
        private Vector3 _cameraVelocity;
        private float _cameraSpeed;

        private bool _playerControlsCamera;

        private Transform _cameraTarget;
        private float _transitionStartTime;
        
        public void SwitchTarget(Transform cameraTarget)
        {
            _cameraTarget = cameraTarget;
            _transitionStartTime = Time.time;
        }
        
        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
            
            if (Settings == null) return;
            
            _camera.transform.localPosition = Settings.InitialTransformData.InitialPosition;
            _camera.transform.localRotation = Quaternion.Euler(new Vector3(Settings.InitialTransformData.InitialRotation.x, 0, 0));
            CameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, Settings.InitialTransformData.InitialRotation.y, 0));
        }

        private void Update()
        {
            if (!CheckIfSettingsFileIsAssigned())
            {
                return;
            }

            Debug.DrawRay(CameraPivot.transform.position, CameraPivot.forward * 10, Color.red);

            CheckIfCameraTargetClicked();

            CaptureKeyboardControls();
            CaptureMouseControls();
            CaptureCameraZoom();
            CaptureMouseRotation();

            LimitCameraMovement();
        }

        private void LateUpdate()
        {
            if (!CheckIfSettingsFileIsAssigned())
            {
                return;
            }

            DampCameraMovement();
            ControlCameraHeight();
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            if (_cameraTarget != null)
            {
                var transitionComplete = (Time.time - _transitionStartTime) / Settings.TransitionTime;
                var origin = new Vector3(CameraPivot.transform.position.x,0, CameraPivot.transform.position.z);
                var destination = new Vector3(_cameraTarget.transform.position.x, 0,
                    _cameraTarget.transform.position.z);

                var target = destination - origin;
                CameraPivot.transform.localPosition = Vector3.Lerp(CameraPivot.transform.localPosition, CameraPivot.transform.localPosition + target, transitionComplete);
                return;
            }
            _cameraVelocity = Vector3.ClampMagnitude(_cameraVelocity, Settings.CameraMovementData.MaxCameraSpeed);
            CameraPivot.localPosition += _cameraVelocity;
            _playerControlsCamera = false;
        }
        
        private void CheckIfCameraTargetClicked()
        {
            if( Input.GetMouseButtonDown(0) )
            {
                var ray = _camera.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;

                if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) return;

                var target = hit.transform.GetComponent<RTSCameraTargetComponent>();
                if (target != null)
                {
                    SwitchTarget(target.transform);
                }
            }
        }
        
        private void CaptureKeyboardControls()
        {
            if (!Settings.EnableKeyboardMovement)
            {
                return;
            }

            var horizontalAxis = Input.GetAxis(Settings.KeyboardData.HorizontalPanAxisName);
            var verticalAxis = Input.GetAxis(Settings.KeyboardData.VerticalPanAxisName);
            
            var localDirection = Vector3.zero;

            localDirection += CameraPivot.forward * verticalAxis;
            localDirection += CameraPivot.right * horizontalAxis;
            
            _cameraVelocity += localDirection * Settings.CameraMovementData.Acceleration;

            if(localDirection.magnitude > 0)
            { 
                _playerControlsCamera = true;
                _cameraTarget = null;
            }
        }
        
        private void CaptureMouseControls()
        {
            if (!Settings.EnableMouseMovement)
            {
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            var scrollVelocity = Vector2.zero;
            var view = _camera.ScreenToViewportPoint(mousePos);

            var isPointerOverUi = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
            var isCursorOutsideOfGameFrame = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;

            if (isCursorOutsideOfGameFrame)
            {
                return;
            }
            var xDelta = 0;
            var yDelta = 0;

            if (!isPointerOverUi)
            {
                if (mousePos.x <= Settings.MouseData.LeftScrollPadding)
                {
                    xDelta = (int)Mathf.Abs(Settings.MouseData.LeftScrollPadding - mousePos.x);
                    scrollVelocity.x -= Settings.CameraMovementData.Acceleration * Time.deltaTime * 0.1f;
                }
                else if (mousePos.x >= Screen.width - Settings.MouseData.RightScrollPadding)
                {
                    xDelta = (int)Mathf.Abs(Screen.width - Settings.MouseData.RightScrollPadding - mousePos.x);

                    scrollVelocity.x += Settings.CameraMovementData.Acceleration * Time.deltaTime * 0.1f;
                }

                if (mousePos.y <= Settings.MouseData.TopScrollPadding)
                {
                    yDelta = (int)Mathf.Abs(Settings.MouseData.TopScrollPadding - mousePos.y);
                    scrollVelocity.y -= Settings.CameraMovementData.Acceleration * Time.deltaTime * 0.1f;
                }
                else if (mousePos.y >= Screen.height - Settings.MouseData.BottomScrollPadding)
                {
                    yDelta = (int)Mathf.Abs(Screen.height - Settings.MouseData.BottomScrollPadding - mousePos.y);
                    scrollVelocity.y += Settings.CameraMovementData.Acceleration * Time.deltaTime * 0.1f;
                }
                scrollVelocity.x *= xDelta;
                scrollVelocity.y *= yDelta;
            }

            if(scrollVelocity.magnitude > 0)
            { 
                _playerControlsCamera = true;
                _cameraTarget = null;
            }

            _cameraVelocity += new Vector3(scrollVelocity.x, 0, scrollVelocity.y);

        }
        private void DampCameraMovement()
        {
            if (_cameraVelocity.magnitude > 0f && !_playerControlsCamera)
            {
                _cameraVelocity *= (1 - Settings.CameraMovementData.Damping);
            }
        }
        
        private void CaptureMouseRotation()
        {
            if (!Settings.EnableMouseRotation)
            {
                return;
            }
            var mouseButtonId = (int) Settings.RotationControlData.RotationTrigger;
            if (Input.GetMouseButtonDown(mouseButtonId))
            {
                Settings.RotationControlData.MouseDownPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(mouseButtonId))
            {
                Settings.RotationControlData.MouseDownPosition = Vector2.zero;
            }
            if (!Input.GetMouseButton((int) Settings.RotationControlData.RotationTrigger))
            {
                return;
            }

            var mouseDelta = Input.mousePosition - Settings.RotationControlData.MouseDownPosition;
            var flipAxis = Settings.RotationControlData.InvertRotationAxis ? 1 : -1;
            CameraPivot.RotateAround(CameraPivot.position, Vector3.down,
                flipAxis * mouseDelta.x * Settings.RotationControlData.RotationSensitivity);
            Settings.RotationControlData.MouseDownPosition = Input.mousePosition;
        }
        private void CaptureCameraZoom()
        {
            if (!Settings.EnableCameraZoom)
            {
                return;
            }

            var scrollValue = Input.GetAxis(Settings.CameraZoomData.ZoomAxisName);
            var fovDelta = scrollValue * Settings.CameraZoomData.ZoomSpeed * Time.deltaTime * 50;
            var newCamFov = Mathf.Clamp(_camera.fieldOfView - fovDelta, Settings.CameraZoomData.MinFOV,
                Settings.CameraZoomData.MaxFOV);

            var cameraZoomPerc = 1 - ((newCamFov - Settings.CameraZoomData.MinFOV) /
                                 (Settings.CameraZoomData.MaxFOV - Settings.CameraZoomData.MinFOV));
            
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newCamFov, Settings.CameraZoomData.ZoomTime);
            // TODO : Camera Pitching

        }
        private void LimitCameraMovement()
        {
            if (!EnableCameraBounds)
            {
                return;
            }

            if (CameraLimitation.CameraAreaCollider == null ||
                CameraLimitation.CameraAreaCollider.bounds.Contains(CameraPivot.position))
            {
                return;
            }

            var closestPointOnBounds =
                CameraLimitation.CameraAreaCollider.ClosestPointOnBounds(CameraPivot.position);
            var forceDirection = closestPointOnBounds - CameraPivot.position;
            
            Debug.DrawRay(CameraPivot.position, forceDirection, Color.magenta);
            _cameraVelocity += forceDirection * CameraLimitation.LimitationForce * Time.deltaTime;
        }

        private void ControlCameraHeight()
        {
            if (!Settings.EnableSurfaceFloating)
            {
                return;
            }
            var newCameraPosition = _camera.transform.localPosition;
            newCameraPosition.z = -Settings.PivotOffset;

            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, Vector3.down, out hit, Mathf.Infinity,
                Settings.SurfaceData.SurfaceMask))
            {
                Debug.DrawRay(_camera.transform.position, Vector3.down * hit.distance, Color.yellow);
                newCameraPosition.y = hit.point.y + Settings.SurfaceData.FloatingHeight;
            }
            else
            {
                OMALog.Warning(
                    "Cannot find a Surface to float on. Check if the Surface Mask is correct and that the CameraRig is ABOVE the surface");
            }
            _camera.transform.localPosition = newCameraPosition;
        }
        
        private bool CheckIfSettingsFileIsAssigned()
        {
            if (Settings != null) return true;

            OMALog.Warning(string.Format(
                "Settings not assigned. Please assign a settings file to " +
                "control the camera. FloatingCameraComponent is assigned on : '{0}' GameObject",
                gameObject.name));
            return false;
        }
    }
}
