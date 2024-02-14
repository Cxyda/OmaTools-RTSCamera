//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Plugins/O.M.A.Games/RTSCamera/Runtime/Settings/Imput/RTSCameraActionMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @RTSCameraActionMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RTSCameraActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RTSCameraActionMap"",
    ""maps"": [
        {
            ""name"": ""CameraControls"",
            ""id"": ""3110a1bc-9b3d-48c5-a4a3-5650827bb8fd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0a40befe-e24a-454c-9d2d-97d8f4b74ebb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""89cfa938-45c5-40c6-a179-e0a81265609f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FreeRotate"",
                    ""type"": ""Value"",
                    ""id"": ""478be2f1-7293-4ae9-9dbe-37cb9ead5aec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StepRotate"",
                    ""type"": ""Value"",
                    ""id"": ""965c987f-e888-4187-bbdc-2d6bfe748869"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RestoreInitialCameraRotation"",
                    ""type"": ""Value"",
                    ""id"": ""b5cf179a-55ba-4659-90fe-fb32d1a7193b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard WASD"",
                    ""id"": ""0efbcfc3-ea82-49a8-bb04-42f6e44bf084"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9e303641-defe-49dc-b3ca-eaaf7fc83fc6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c777a00-4f52-4a5a-b4c5-d8ac9ddecb85"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""291fcb83-7933-41a3-9c4b-d3ec49e50ec7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2b2bddb-a6af-46d5-ad4c-09a0dee6e60c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6d0f43a3-7640-4dc2-bad5-5f9cfb8e2ef1"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1,max=1),Invert"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""FreeRotateMouse"",
                    ""id"": ""f16b7e1e-0be3-4c65-93e8-9e3992879400"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeRotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""f9a3b37d-9bb9-4406-9c79-7ba4b4e988af"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""e9ec48f9-5759-44d2-afef-ee9216287fe5"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d45d48d6-46cd-455a-bed6-d142a9ef65a8"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StepRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c65c2079-de02-4fc2-b10c-d12c4b5875b1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""StepRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca79b93b-642f-4542-bc04-055baa57d19a"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestoreInitialCameraRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CameraControls
        m_CameraControls = asset.FindActionMap("CameraControls", throwIfNotFound: true);
        m_CameraControls_Move = m_CameraControls.FindAction("Move", throwIfNotFound: true);
        m_CameraControls_Zoom = m_CameraControls.FindAction("Zoom", throwIfNotFound: true);
        m_CameraControls_FreeRotate = m_CameraControls.FindAction("FreeRotate", throwIfNotFound: true);
        m_CameraControls_StepRotate = m_CameraControls.FindAction("StepRotate", throwIfNotFound: true);
        m_CameraControls_RestoreInitialCameraRotation = m_CameraControls.FindAction("RestoreInitialCameraRotation", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CameraControls
    private readonly InputActionMap m_CameraControls;
    private List<ICameraControlsActions> m_CameraControlsActionsCallbackInterfaces = new List<ICameraControlsActions>();
    private readonly InputAction m_CameraControls_Move;
    private readonly InputAction m_CameraControls_Zoom;
    private readonly InputAction m_CameraControls_FreeRotate;
    private readonly InputAction m_CameraControls_StepRotate;
    private readonly InputAction m_CameraControls_RestoreInitialCameraRotation;
    public struct CameraControlsActions
    {
        private @RTSCameraActionMap m_Wrapper;
        public CameraControlsActions(@RTSCameraActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CameraControls_Move;
        public InputAction @Zoom => m_Wrapper.m_CameraControls_Zoom;
        public InputAction @FreeRotate => m_Wrapper.m_CameraControls_FreeRotate;
        public InputAction @StepRotate => m_Wrapper.m_CameraControls_StepRotate;
        public InputAction @RestoreInitialCameraRotation => m_Wrapper.m_CameraControls_RestoreInitialCameraRotation;
        public InputActionMap Get() { return m_Wrapper.m_CameraControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraControlsActions set) { return set.Get(); }
        public void AddCallbacks(ICameraControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @FreeRotate.started += instance.OnFreeRotate;
            @FreeRotate.performed += instance.OnFreeRotate;
            @FreeRotate.canceled += instance.OnFreeRotate;
            @StepRotate.started += instance.OnStepRotate;
            @StepRotate.performed += instance.OnStepRotate;
            @StepRotate.canceled += instance.OnStepRotate;
            @RestoreInitialCameraRotation.started += instance.OnRestoreInitialCameraRotation;
            @RestoreInitialCameraRotation.performed += instance.OnRestoreInitialCameraRotation;
            @RestoreInitialCameraRotation.canceled += instance.OnRestoreInitialCameraRotation;
        }

        private void UnregisterCallbacks(ICameraControlsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @FreeRotate.started -= instance.OnFreeRotate;
            @FreeRotate.performed -= instance.OnFreeRotate;
            @FreeRotate.canceled -= instance.OnFreeRotate;
            @StepRotate.started -= instance.OnStepRotate;
            @StepRotate.performed -= instance.OnStepRotate;
            @StepRotate.canceled -= instance.OnStepRotate;
            @RestoreInitialCameraRotation.started -= instance.OnRestoreInitialCameraRotation;
            @RestoreInitialCameraRotation.performed -= instance.OnRestoreInitialCameraRotation;
            @RestoreInitialCameraRotation.canceled -= instance.OnRestoreInitialCameraRotation;
        }

        public void RemoveCallbacks(ICameraControlsActions instance)
        {
            if (m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraControlsActions @CameraControls => new CameraControlsActions(this);
    public interface ICameraControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnFreeRotate(InputAction.CallbackContext context);
        void OnStepRotate(InputAction.CallbackContext context);
        void OnRestoreInitialCameraRotation(InputAction.CallbackContext context);
    }
}