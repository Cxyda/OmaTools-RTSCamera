using System;
using Plugins.O.M.A.Games.RTSCamera.Runtime.Utility;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Core
{
    [RequireComponent(typeof(RTSCameraInputComponent))]
    public class RTSCameraComponent : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private RtsCameraSettings _settings;

        [SerializeField] private RTSCameraBoundsVolume _cameraBounds;
        [SerializeField] private bool _drawDebugGizmos;

        [Header("Camera Follow")]
        [SerializeField] private GameObject _cameraFollowTarget;
        [SerializeField] private float _cameraFollowSpeed = 5f;

        private Vector3 _targetRotation;
        private Vector3 _targetLocation;
        private float _targetZoomDistance;
        private Vector3 _worldTransformDirection;
        private Vector3 _movementVelocity;
        private Vector3 _accelerationVector;
        private Vector2 _freeLookTarget;

        private float _targetStepRotationAngle;
        private bool _isStepRotationInProgress;

        [SerializeField, HideInInspector] private Camera _camera;
        [SerializeField, HideInInspector] private Transform _cameraFocusTarget;
        [SerializeField, HideInInspector] private RTSCameraInputComponent _inputComponent;

        // Initial values stored to eventually reset them later
        private Quaternion _initialRotation;
        private Quaternion _initialCameraTargetRotation;
        private float _initialZoomDistance;
        private Vector2 _freeLookRotation;
        private float _targetYawAngle;
        private float _targetPitchAngle;

        public RtsCameraSettings CameraSettings
        {
            get => _settings;
            set
            {
                _settings = value;
                InputComponent.CameraSettings = value;
            }
        }

        protected RTSCameraInputComponent InputComponent
        {
            get
            {
                if(_inputComponent == null)
                {
                    _inputComponent = GetComponent<RTSCameraInputComponent>();
                }
                return _inputComponent;
            }
        }

        public RTSCameraBoundsVolume BoundsVolume
        {
            get => _cameraBounds;
            set => _cameraBounds = value;
        }

        private void Awake()
        {
            InputComponent.CameraSettings = _settings;
            InputComponent.OnCameraMoveActionEvent += OnMove;
            InputComponent.OnFreeRotationActionEvent += OnFreeRotate;
            InputComponent.OnCameraZoomActionEvent += OnZoom;
            InputComponent.OnStepRotationActionEvent += OnStepRotate;
            InputComponent.OnRestoreInitialRotationActionEvent += OnInitialRotationRestored;

            if (!_camera)
            {
                _camera = GetComponentInChildren<Camera>();
            }

            if (!_cameraFocusTarget)
            {
                _cameraFocusTarget = transform.FindChildWithTag(_settings.CameraTargetTagName);
            }
            _targetZoomDistance = Mathf.Abs(_camera.transform.localPosition.z);

            _initialRotation = transform.rotation;
            _initialCameraTargetRotation = _cameraFocusTarget.rotation;
            _initialZoomDistance = _targetZoomDistance;
            
            RestoreInitialRotationAndZoom();
        }

        private void OnEnable()
        {
            InputComponent.Enable(true);
        }

        private void OnDisable()
        {
            InputComponent.Enable(false);
        }

        public void SetCameraFollowTarget(GameObject objectToFollow)
        {
            _cameraFollowTarget = objectToFollow;
        }

        private void LateUpdate()
        {
            UpdateCameraPosition();
            UpdateCameraZoom();
            UpdateCameraFreeLook();
            UpdateCameraStepRotation();

            FollowCameraTarget();
            UpdateTerrainPosition();

            _worldTransformDirection = Vector3.zero;
            _freeLookRotation *= 1 - _settings.LookTurnSpeedDamping;
        }

        private void FollowCameraTarget()
        {
            if(!_cameraFollowTarget) return;
            var targetPosition = _cameraFollowTarget.transform.position;

            var position = transform.position;
            var smoothedTargetPosition = Vector3.SmoothDamp(position, targetPosition,
                ref _movementVelocity, 1f / _cameraFollowSpeed);
            transform.position = smoothedTargetPosition;
        }

        private void OnDrawGizmosSelected()
        {
            if(!_drawDebugGizmos) return;
            var gizmoColor = Gizmos.color;
            Gizmos.color = Color.green;

            if (_cameraFocusTarget)
            {
                Gizmos.DrawWireSphere(_cameraFocusTarget.position, 0.5f);
            }

            if (_cameraBounds)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(_cameraBounds.transform.position, _cameraBounds.transform.localScale);
            }
            Gizmos.color = gizmoColor;
        }

        private void UpdateTerrainPosition()
        {
            const float distance = 10000;
            var cameraTargetPosition = _cameraFocusTarget.transform.position;
            var startPoint = cameraTargetPosition + Vector3.up * (distance * 0.5f);

            if (Physics.Raycast(startPoint, Vector3.down, out var raycastHit, _settings.TerrainRaycastDistance,
                    _settings.TerrainLayer, QueryTriggerInteraction.Ignore))
            {
                var parentTransform = transform;
                var parentPosition = parentTransform.position;
                parentTransform.position = Vector3.Lerp(parentPosition,
                    new Vector3(parentPosition.x, raycastHit.point.y, parentPosition.z),
                    _settings.TerrainFollowSpeed * Time.deltaTime);
            }
        }

        private void UpdateCameraStepRotation()
        {
            if(!_isStepRotationInProgress) return;

            var rotation = transform.rotation;

            var targetRotation =
                Quaternion.Euler(rotation.eulerAngles.x, _targetStepRotationAngle, rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(rotation,targetRotation, _settings.StepTurnSpeed * Time.deltaTime);
            var angle = Quaternion.Angle(rotation, targetRotation);

            if (angle <= _settings.RotationSnapAngle)
            {
                transform.rotation = targetRotation;
                _targetYawAngle = targetRotation.eulerAngles.y;
                _isStepRotationInProgress = false;
            }
        }

        private void UpdateCameraFreeLook()
        {
            if(_isStepRotationInProgress) return;

            // Parent object rotation (yaw)
            var rotation = transform.rotation;
            var targetRotation = Quaternion.Euler(rotation.eulerAngles.x, _targetYawAngle, rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(rotation, targetRotation, _settings.LookTurnSpeed * Time.deltaTime);

            // camera rotation (pitch)
            var cameraTargetRotation = _cameraFocusTarget.transform.rotation;
            _targetPitchAngle = Mathf.Clamp(_targetPitchAngle, _settings.MinPitchAngle, _settings.MaxPitchAngle);
            var targetCameraRotation = Quaternion.Euler(_targetPitchAngle, cameraTargetRotation.eulerAngles.y, cameraTargetRotation.eulerAngles.z);

            _cameraFocusTarget.transform.rotation =  Quaternion.Lerp(cameraTargetRotation, targetCameraRotation, _settings.LookTurnSpeed * Time.deltaTime);
        }

        private void UpdateCameraZoom()
        {
            var cameraLocalPosition = _camera.transform.localPosition;
            _targetZoomDistance = Mathf.Clamp(_targetZoomDistance, _settings.MinZoomDistance, _settings.MaxZoomDistance);
            _camera.transform.localPosition = Vector3.Lerp(cameraLocalPosition, new Vector3(cameraLocalPosition.x, cameraLocalPosition.y, -_targetZoomDistance),
                _settings.ZoomSpeed * Time.deltaTime);
        }

        private void UpdateCameraPosition()
        {
            var localPosition = transform.localPosition;
            var targetPosition = localPosition + _worldTransformDirection;
            
            var smoothedTargetPosition = Vector3.SmoothDamp(localPosition, targetPosition,
                ref _movementVelocity, 1f / _settings.MovementAcceleration, _settings.MaxMovementSpeed);

            if (!_cameraBounds || _cameraBounds.IsTargetWithinBounds(smoothedTargetPosition))
            {
                transform.localPosition = smoothedTargetPosition;
            }
            else
            {
                var closestPoint = _cameraBounds.GetClosestPointOnBounds(smoothedTargetPosition);
                var pushbackVector = (closestPoint - smoothedTargetPosition) * _settings.BoundsPushbackForce * 0.1f;
                smoothedTargetPosition = Vector3.SmoothDamp(localPosition, smoothedTargetPosition + pushbackVector,
                    ref _movementVelocity, 1f / _settings.MovementAcceleration, _settings.MaxMovementSpeed);
                transform.localPosition = smoothedTargetPosition;
            }
        }

        private void RestoreInitialRotationAndZoom()
        {
            _targetYawAngle = _initialRotation.eulerAngles.y;
            _targetPitchAngle = _initialCameraTargetRotation.eulerAngles.x;
            _targetZoomDistance = _initialZoomDistance;
        }

        private void OnMove(Vector2 inputValue)
        {
            if(_cameraFollowTarget) return;

            _worldTransformDirection = transform.TransformDirection(new Vector3(-inputValue.x, 0, inputValue.y)) *
                                       _settings.MaxMovementSpeed;
        }

        private void OnZoom(float zoomDirection)
        {
            _targetZoomDistance += zoomDirection * _settings.ZoomStepSize;
        }
        
        private void OnInitialRotationRestored()
        {
            RestoreInitialRotationAndZoom();
        }

        private void OnStepRotate(float stepDirection)
        {
            var closestStepIndex = Mathf.RoundToInt(transform.rotation.eulerAngles.y / _settings.StepTurnAngle);

            // if angle <= 10° advance to the next step
            if ( Mathf.Abs(transform.rotation.eulerAngles.y - closestStepIndex * _settings.StepTurnAngle) <= _settings.NextTurnStepAngleThreshold)
            {
                _targetStepRotationAngle = (closestStepIndex + stepDirection) * _settings.StepTurnAngle;
            }

            _isStepRotationInProgress = true;
        }

        private void OnFreeRotate(Vector2 inputDelta)
        {
            _isStepRotationInProgress = false;
            _freeLookRotation += inputDelta;
            _targetYawAngle = transform.rotation.eulerAngles.y + _freeLookRotation.x;
            _targetPitchAngle = _cameraFocusTarget.eulerAngles.x + _freeLookRotation.y;
        }
    }
}