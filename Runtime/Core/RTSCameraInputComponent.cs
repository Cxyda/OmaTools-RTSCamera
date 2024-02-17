using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Core
{
    public class RTSCameraInputComponent : MonoBehaviour
    {
        public event Action<Vector2> OnCameraMoveActionEvent;
        public event Action<Vector2> OnFreeRotationActionEvent;
        public event Action<float> OnCameraZoomActionEvent;
        public event Action<float> OnStepRotationActionEvent;
        public event Action OnRestoreInitialRotationActionEvent;

        [SerializeField] private RtsCameraSettings _settings;

        [FormerlySerializedAs("MoveCameraForwardKey")]
        [Header("Old Input System Settings")]
        [SerializeField] private KeyCode _moveCameraForwardKey = KeyCode.W;
        [SerializeField] private KeyCode _moveCameraBackwardsKey = KeyCode.S;
        [SerializeField] private KeyCode _moveCameraLeftKey = KeyCode.A;
        [SerializeField] private KeyCode _moveCameraRightKey = KeyCode.D;
        [SerializeField] private KeyCode _stepRotateCameraLeftKey = KeyCode.Q;
        [SerializeField] private KeyCode _stepRotateCameraRightKey = KeyCode.E;
        [SerializeField] private KeyCode _resetCameraRotationKey = KeyCode.Tilde;
        [SerializeField] private KeyCode _freeCameraRotationKey = KeyCode.Mouse2;
        [SerializeField] private bool _inverseFreeCameraLookYAxis = true;
        [SerializeField] private string _cameraZoomAxisKey = "Mouse ScrollWheel";
        [SerializeField] private bool _inverseScrollDirection = true;

        [Header("New Input System Settings")]
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private string _inputActionMapName = "CameraControls";
        
        [SerializeField] private string _moveActionName = "Move";
        [SerializeField] private string _zoomActionName = "Zoom";
        [SerializeField] private string _freeRotationActionName = "FreeRotate";
        [SerializeField] private string _stepRotationActionName = "StepRotate";
        [SerializeField] private string _restoreInitialRotationActionName = "RestoreInitialRotation";

        private bool _isUsingNewInputSystem;
        
        private InputActionMap _inputActionMap;
        private InputAction _moveAction;
        private InputAction _zoomAction;
        private InputAction _freeRotateAction;
        private InputAction _stepRotateAction;
        private InputAction _restoreInitialRotationAction;
        
        private Vector2 _lastMouseCursorPosition;
        private bool _isInputEnabled;

        public RtsCameraSettings CameraSettings
        {
            get => _settings; 
            set => _settings = value;
        }

        protected virtual void Awake()
        {
            SwitchInputActionAsset(_inputActionAsset);
            if (_isUsingNewInputSystem)
            {
                GetActionsAndRegisterBindings();
            }
        }

        protected virtual void Update()
        {
            if(!_isInputEnabled) return;

            if (_isUsingNewInputSystem)
            {
                if (_moveAction.IsPressed())
                {
                    OnCameraMoveActionEvent?.Invoke(_moveAction.ReadValue<Vector2>());
                }

                if (_freeRotateAction.IsPressed())
                {
                    OnFreeRotationActionEvent?.Invoke(_freeRotateAction.ReadValue<Vector2>());
                }
            }
            else
            {
                if (Input.GetKey(_moveCameraForwardKey) || Input.GetKey(_moveCameraBackwardsKey) || Input.GetKey(_moveCameraLeftKey) || Input.GetKey(_moveCameraRightKey))
                {
                    var moveVector = new Vector2
                    {
                        x = Input.GetKey(_moveCameraRightKey) ? -1 : Input.GetKey(_moveCameraLeftKey) ? 1 : 0,
                        y = Input.GetKey(_moveCameraForwardKey) ? 1 : Input.GetKey(_moveCameraBackwardsKey) ? -1 : 0
                    };
                    OnCameraMoveActionEvent?.Invoke(moveVector);
                }

                if (Input.GetKeyDown(_freeCameraRotationKey))
                {
                    _lastMouseCursorPosition = (Vector2) Input.mousePosition;
                }

                if (Input.GetKey(_freeCameraRotationKey))
                {
                    var mouseDelta = _lastMouseCursorPosition - (Vector2) Input.mousePosition;
                    mouseDelta.x *= -1;
                    if (_inverseFreeCameraLookYAxis)
                    {
                        mouseDelta.y *= -1;
                    }
                    OnFreeRotationActionEvent?.Invoke(mouseDelta);
                    
                    _lastMouseCursorPosition = (Vector2) Input.mousePosition;
                }

                if (Math.Abs(Input.GetAxis(_cameraZoomAxisKey)) > float.Epsilon)
                {
                    float multiplier = _inverseScrollDirection ? -1 : 1;
                    var axisValue = Input.GetAxis(_cameraZoomAxisKey) * 10;
                    OnCameraZoomActionEvent?.Invoke(axisValue * multiplier);
                }

                if (Input.GetKeyDown(_resetCameraRotationKey))
                {
                    OnRestoreInitialRotationActionEvent?.Invoke();
                }

                if (Input.GetKeyDown(_stepRotateCameraLeftKey) || Input.GetKeyDown(_stepRotateCameraRightKey))
                {
                    float direction = Input.GetKeyDown(_stepRotateCameraLeftKey) ? 1 : -1;
                    OnStepRotationActionEvent?.Invoke(direction);
                }
            }

            if (_settings.AllowScreenScroll)
            {
                var cursorPosition = Input.mousePosition;
                var screenWidth = Screen.width;
                var screenHeight = Screen.height;
                if(cursorPosition.x < screenWidth * _settings.LeftScrollArea)
                {
                    OnCameraMoveActionEvent?.Invoke(new Vector2(1, 0));
                }
                else if(cursorPosition.x > screenWidth * _settings.RightScrollArea)
                {
                    OnCameraMoveActionEvent?.Invoke(new Vector2(-1, 0));
                }
                
                if(cursorPosition.y < screenHeight * _settings.BottomScrollArea)
                {
                    OnCameraMoveActionEvent?.Invoke(new Vector2(0, -1));
                }
                else if(cursorPosition.y > screenHeight * _settings.TopScrollArea)
                {
                    OnCameraMoveActionEvent?.Invoke(new Vector2(0, 1));
                }
            }
        }

        public virtual void SwitchInputActionAsset(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
            _inputActionMap = _inputActionAsset ? _inputActionAsset.FindActionMap(_inputActionMapName) : null;
            _isUsingNewInputSystem = _inputActionAsset && _inputActionMap != null;
            if (_isUsingNewInputSystem)
            {
                _inputActionAsset.Enable();
            }
        }

        protected virtual void GetActionsAndRegisterBindings()
        {
            _moveAction = _inputActionMap.FindAction(_moveActionName, true);
            _zoomAction = _inputActionMap.FindAction(_zoomActionName, true);
            
            _freeRotateAction = _inputActionMap.FindAction(_freeRotationActionName, true);
            _stepRotateAction = _inputActionMap.FindAction(_stepRotationActionName, true);
            _restoreInitialRotationAction = _inputActionMap.FindAction(_restoreInitialRotationActionName, true);

            _moveAction.performed += OnMove;
            _zoomAction.performed += OnZoom;
            _stepRotateAction.performed += OnStepRotate;
            _restoreInitialRotationAction.performed += OnInitialRotationRestored;
        }

        protected virtual void OnDestroy()
        {
            if (_isUsingNewInputSystem)
            {
                _moveAction.performed -= OnMove;
                _zoomAction.performed -= OnZoom;
                _stepRotateAction.performed -= OnStepRotate;
                _restoreInitialRotationAction.performed -= OnInitialRotationRestored;
            }
        }

        protected virtual void OnInitialRotationRestored(InputAction.CallbackContext context)
        {
            if(!context.performed) return;

            OnRestoreInitialRotationActionEvent?.Invoke();
        }

        protected virtual void OnStepRotate(InputAction.CallbackContext context)
        {
            if(!context.performed) return;

            OnStepRotationActionEvent?.Invoke(context.ReadValue<float>());
        }

        protected virtual void OnZoom(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

            OnCameraZoomActionEvent?.Invoke(context.ReadValue<float>());
        }

        protected virtual void OnMove(InputAction.CallbackContext context)
        {
            if(!context.performed) return;

            OnCameraMoveActionEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public virtual void Enable(bool isEnabled)
        {
            _isInputEnabled = isEnabled;

            if(isEnabled)
            {
                _inputActionMap?.Enable();
            }
            else
            {
                _inputActionMap?.Disable();
            }
        }
    }
}