using Plugins.O.M.A.Games.RTSCamera.Runtime.Core;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime
{
    /// <summary>
    /// This class is responsible for creating the RTSCameraRig
    /// </summary>
    public static class RTSCameraCreator
    {
#if UNITY_EDITOR
        [MenuItem("O.M.A.Tools/Cameras/Create RTSCamera")]
        [MenuItem("GameObject/O.M.A.Tools/Camera/Create RTSCamera", priority = -201)]
        public static void CreateRTSCameraRig()
        {
            var settings = LoadOrCreateSettings();

            var cameraRig = new GameObject("RTSCameraRig");
            var rtsCameraInputComponent = cameraRig.AddComponent<RTSCameraInputComponent>();
            var rtsCameraComponent = cameraRig.AddComponent<RTSCameraComponent>();

            rtsCameraComponent.CameraSettings = settings;
            cameraRig.transform.localRotation = Quaternion.Euler(new Vector3(0, 45, 0));

            var cameraTarget = new GameObject("CameraTarget");
            cameraTarget.transform.SetParent(cameraRig.transform, true);
            cameraTarget.transform.localPosition = new Vector3(0, 0, 0);
            cameraTarget.transform.localRotation = Quaternion.Euler(new Vector3(60, 0, 0));

            cameraTarget.tag = settings.CameraTargetTagName;
            var cameraObject = new GameObject("Camera", typeof(Camera));

            cameraObject.transform.SetParent(cameraTarget.transform, true);
            cameraObject.transform.localPosition = new Vector3(0, 0, -25);
            cameraObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        [MenuItem("O.M.A.Tools/Cameras/Create RTSCamera Settings")]
        public static void CreateRTSCameraSettings()
        {
            var settings = ScriptableObject.CreateInstance<RtsCameraSettings>();
            AssetDatabase.CreateAsset(settings, "Assets/Resources/RTSCameraSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(settings);
        }

        [MenuItem("GameObject/O.M.A.Tools/Camera/Create Camera Bounds", priority = -200)]
        public static void CreateRTSCameraBounds()
        {
            var cameraBoundsObject = new GameObject("CameraBounds")
                {
                    transform =
                    {
                        localScale = new Vector3(50, 20, 50)
                    },
                    layer = LayerMask.NameToLayer("Ignore Raycast"),
                };
            var boxCollider = cameraBoundsObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            var rtsCameraBoundsVolume = cameraBoundsObject.AddComponent<RTSCameraBoundsVolume>();
            var rtsCameraComponent = Object.FindObjectOfType<RTSCameraComponent>();
            if (rtsCameraComponent != null)
            {
                rtsCameraComponent.BoundsVolume = rtsCameraBoundsVolume;
            }
        }
        private static RtsCameraSettings LoadOrCreateSettings()
        {
            var settingsArray = Resources.LoadAll<RtsCameraSettings>("Settings");
            if (settingsArray != null && settingsArray.Length > 0) return settingsArray[0];

            var settings = ScriptableObject.CreateInstance<RtsCameraSettings>();
            AssetDatabase.CreateAsset(settings, "Assets/Resources/RTSCameraSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
#endif
    }
}