using System.Collections.Generic;
using UnityEditor;

namespace Plugins.O.M.A.Games.RTSCamera.Editor
{
    [CustomEditor(typeof(RTSCameraComponent))]
    public class RTSCameraComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _enableAreaBounds;
        private SerializedProperty _cameraLimitationData;
        
        private void OnEnable()
        {
            _cameraLimitationData = serializedObject.FindProperty("CameraLimitation");
            _cameraLimitationData.isExpanded = true;
        }

        public override void OnInspectorGUI()
        {
            var myTarget = (RTSCameraComponent)target;
            
            if (!myTarget.EnableCameraBounds)
            {
                DrawPropertiesExcluding(serializedObject, "CameraLimitation");
            }
            else
            {
                DrawDefaultInspector();
            }
 
            serializedObject.ApplyModifiedProperties();
        }
    }
}