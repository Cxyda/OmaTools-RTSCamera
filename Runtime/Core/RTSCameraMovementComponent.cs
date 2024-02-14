using Plugins.O.M.A.Games.RTSCamera.Runtime.Utility;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Core
{
    public class RTSCameraMovementComponent : MonoBehaviour
    {
        [Header("Input Component")]
        [SerializeField] private RTSCameraInputComponent _inputComponent;

        [Header("Movement")]
        [SerializeField] private float _maxMovementSpeed = 5.0f;
        [Range(0.1f, 10f)]
        [SerializeField] private float _movementAcceleration = 2.5f;

        [Header("Zoom")]
        [SerializeField]private float _zoomStepSize = 5f;
        [SerializeField] private float _zoomSpeed = 10.0f;
        [SerializeField] private float _minZoomDistance = 50.0f;
        [SerializeField] private float _maxZoomDistance = 500.0f;

        [Header("Look")]
        [SerializeField] private float _lookTurnSpeed = 2.0f;
        [SerializeField] private float _lookTurnSpeedDamping = 0.05f;
        [SerializeField] private float _stepTurnAngle = 45.0f;
        [SerializeField] private float _stepTurnSpeed = 5.0f;
        [SerializeField] private float _minPitchAngle = 20f;
        [SerializeField] private float _maxPitchAngle = 90f;

        [Header("Optional Settings")]
        [SerializeField] private LayerMask _terrainLayer;
        [SerializeField] private float _terrainFollowSpeed = 5f;

        [SerializeField] private RTSCameraBoundsVolume _cameraBounds;
        [Range(1f, 100f)]
        [SerializeField] private float _boundsPushbackForce = 10f;

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

        // Initial values stored to eventually reset them later
        private Quaternion _initialRotation;
        private Quaternion _initialCameraTargetRotation;
        private float _initialZoomDistance;
        private Vector2 _freeLookRotation;
        private float _targetYawAngle;
        private float _targetPitchAngle;

        private void Awake()
        {
            _inputComponent.OnCameraMoveActionEvent += OnMove;
            _inputComponent.OnFreeRotationActionEvent += OnFreeRotate;
            _inputComponent.OnCameraZoomActionEvent += OnZoom;
            _inputComponent.OnStepRotationActionEvent += OnStepRotate;
            _inputComponent.OnRestoreInitialRotationActionEvent += OnInitialRotationRestored;

            if (!_camera)
            {
                _camera = GetComponentInChildren<Camera>();
            }

            if (!_cameraFocusTarget)
            {
                _cameraFocusTarget = transform.FindChildWithTag(RTSCamera_GlobalSettings.CameraTargetTagName);
            }
            _targetZoomDistance = Mathf.Abs(_camera.transform.localPosition.z);

            _initialRotation = transform.rotation;
            _initialCameraTargetRotation = _cameraFocusTarget.rotation;
            _initialZoomDistance = _targetZoomDistance;
            
            RestoreInitialRotationAndZoom();
        }

        private void OnEnable()
        {
            _inputComponent.Enable(true);
        }

        private void OnDisable()
        {
            _inputComponent.Enable(false);
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
            _freeLookRotation *= 1 - _lookTurnSpeedDamping;
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

            if (Physics.Raycast(startPoint, Vector3.down, out var raycastHit,RTSCamera_GlobalSettings.RaycastDistance,  _terrainLayer,
                    QueryTriggerInteraction.Ignore))
            {
                var parentTransform = transform;
                var parentPosition = parentTransform.position;
                parentTransform.position = Vector3.Lerp(parentPosition, new Vector3(parentPosition.x, raycastHit.point.y, parentPosition.z), _terrainFollowSpeed * Time.deltaTime);
            }
        }

        private void UpdateCameraStepRotation()
        {
            if(!_isStepRotationInProgress) return;

            var rotation = transform.rotation;

            var targetRotation =
                Quaternion.Euler(rotation.eulerAngles.x, _targetStepRotationAngle, rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(rotation,targetRotation, _stepTurnSpeed * Time.deltaTime);
            var angle = Quaternion.Angle(rotation, targetRotation);

            if (angle <= RTSCamera_GlobalSettings.RotationSnapAngle)
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
            transform.rotation = Quaternion.Lerp(rotation, targetRotation, _lookTurnSpeed * Time.deltaTime);

            // camera rotation (pitch)
            var cameraTargetRotation = _cameraFocusTarget.transform.rotation;
            _targetPitchAngle = Mathf.Clamp(_targetPitchAngle, _minPitchAngle, _maxPitchAngle);
            var targetCameraRotation = Quaternion.Euler(_targetPitchAngle, cameraTargetRotation.eulerAngles.y, cameraTargetRotation.eulerAngles.z);

            _cameraFocusTarget.transform.rotation =  Quaternion.Lerp(cameraTargetRotation, targetCameraRotation, _lookTurnSpeed * Time.deltaTime);
        }

        private void UpdateCameraZoom()
        {
            var cameraLocalPosition = _camera.transform.localPosition;
            _targetZoomDistance = Mathf.Clamp(_targetZoomDistance, _minZoomDistance, _maxZoomDistance);
            _camera.transform.localPosition = Vector3.Lerp(cameraLocalPosition, new Vector3(cameraLocalPosition.x, cameraLocalPosition.y, -_targetZoomDistance),
                _zoomSpeed * Time.deltaTime);
        }

        private void UpdateCameraPosition()
        {
            var localPosition = transform.localPosition;
            var targetPosition = localPosition + _worldTransformDirection;
            
            var smoothedTargetPosition = Vector3.SmoothDamp(localPosition, targetPosition,
                ref _movementVelocity, 1f / _movementAcceleration, _maxMovementSpeed);

            if (!_cameraBounds || _cameraBounds.IsTargetWithinBounds(smoothedTargetPosition))
            {
                transform.localPosition = smoothedTargetPosition;
            }
            else
            {
                var closestPoint = _cameraBounds.GetClosestPointOnBounds(smoothedTargetPosition);
                var pushbackVector = (closestPoint - smoothedTargetPosition) * _boundsPushbackForce * 0.1f;
                smoothedTargetPosition = Vector3.SmoothDamp(localPosition, smoothedTargetPosition + pushbackVector,
                    ref _movementVelocity, 1f / _movementAcceleration, _maxMovementSpeed);
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
                                          _maxMovementSpeed;
        }

        private void OnZoom(float zoomDirection)
        {
            _targetZoomDistance += zoomDirection * _zoomStepSize;
        }
        
        private void OnInitialRotationRestored()
        {
            RestoreInitialRotationAndZoom();
        }

        private void OnStepRotate(float stepDirection)
        {
            var closestStepIndex = Mathf.RoundToInt(transform.rotation.eulerAngles.y / _stepTurnAngle);

            // if angle <= 10° advance to the next step
            if ( Mathf.Abs(transform.rotation.eulerAngles.y - closestStepIndex * _stepTurnAngle) <= RTSCamera_GlobalSettings.NextTurnStepAngleThreshold)
            {
                _targetStepRotationAngle = (closestStepIndex + stepDirection) * _stepTurnAngle;
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