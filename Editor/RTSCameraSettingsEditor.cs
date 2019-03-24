using System.Collections.Generic;
using Plugins.O.M.A.Games.RTS_Camera.Core;
using UnityEditor;

namespace Plugins.O.M.A.Games.RTSCamera.Editor
{
    [CustomEditor(typeof(RTSCameraSettings))]
    [CanEditMultipleObjects]
    public class RTSCameraSettingsEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableSurfaceFloatingData;
        private SerializedProperty _surfaceFloatingData;
        
        private SerializedProperty _movementData;
        private SerializedProperty _keyboardData;
        private SerializedProperty _mouseData;
        
        private SerializedProperty _cameraZoomData;
        private SerializedProperty _mouseRotationData;

        private void OnEnable()
        {
            _surfaceFloatingData = serializedObject.FindProperty("SurfaceData");

            _movementData = serializedObject.FindProperty("CameraMovementData");
            _keyboardData = serializedObject.FindProperty("KeyboardData");
            _mouseData = serializedObject.FindProperty("MouseData");
            _cameraZoomData = serializedObject.FindProperty("CameraZoomData");
            _mouseRotationData = serializedObject.FindProperty("RotationControlData");
            
            _surfaceFloatingData.isExpanded = true;
            _keyboardData.isExpanded = true;
            _mouseData.isExpanded = true;
            _cameraZoomData.isExpanded = true;
            _mouseRotationData.isExpanded = true;
        }

        public override void OnInspectorGUI()
        {
            var myTarget = (RTSCameraSettings)target;
            var options = new List<string>();
            
            if (!myTarget.EnableKeyboardMovement && !myTarget.EnableMouseMovement)
            {
                options.Add("CameraMovementData");
            }
            
            if (!myTarget.EnableSurfaceFloating)
            {
                options.Add("SurfaceData");
            }
            if (!myTarget.EnableKeyboardMovement)
            {
                options.Add("KeyboardData");
            }
            if (!myTarget.EnableMouseMovement)
            {
                options.Add("MouseData");
            }
            if (!myTarget.EnableCameraZoom)
            {
                options.Add("CameraZoomData");
            }
            if (!myTarget.EnableMouseRotation)
            {
                options.Add("RotationControlData");
            }
            DrawPropertiesExcluding(serializedObject, options.ToArray());

            serializedObject.ApplyModifiedProperties();
        }
    }
}