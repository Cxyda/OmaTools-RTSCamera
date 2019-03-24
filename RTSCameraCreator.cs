using Plugins.O.M.A.Games.Core;
using Plugins.O.M.A.Games.RTS_Camera.Core;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTS_Camera
{
    /// <summary>
    /// This class is responsible for creating the RTSCameraRig
    /// </summary>
    public static class RTSCameraCreator
    {
#if UNITY_EDITOR
        private static readonly Vector3 InitialSpawnPosition = new Vector3(0,50,0);
        private static readonly Vector3 InitialBoundingBox = new Vector3(100,50,100);
        
        [MenuItem("O.M.A.Tools/Cameras/Create RTSCamera")]
        public static void CreateFloatingCameraRig()
        {
            var cameraRig = new GameObject("RTSCameraRig");

            var floatingCamera = new GameObject("RTSCamera", typeof(RTSCameraComponent));
            var cameraPivot = new GameObject("CameraPivot");
            
            var cameraBounds = new GameObject("CameraBounds", typeof(BoxCollider));
            var camera = new GameObject("Camera", typeof(Camera));

            cameraPivot.transform.SetParent(floatingCamera.transform, true);
            
            floatingCamera.transform.SetParent(cameraRig.transform, true);
            cameraBounds.transform.SetParent(cameraRig.transform, true);
            
            camera.transform.SetParent(cameraPivot.transform, true);

            camera.tag = "MainCamera";

            cameraRig.transform.localPosition = InitialSpawnPosition;

            var floatingCameraComp = floatingCamera.GetComponent<RTSCameraComponent>();
            var cameraBoundsCollider = cameraBounds.GetComponent<BoxCollider>();

            floatingCameraComp.CameraPivot = cameraPivot.transform;
            floatingCameraComp.Settings = OMAUtils.GetFloatingCameraSettings<RTSCameraSettings>();

            floatingCameraComp.CameraLimitation.CameraAreaCollider = cameraBoundsCollider;
            cameraBoundsCollider.size = InitialBoundingBox;

            camera.transform.localPosition = floatingCameraComp.Settings.InitialTransformData.InitialPosition;
            camera.transform.localRotation = Quaternion.Euler(floatingCameraComp.Settings.InitialTransformData.InitialRotation);
        }
#endif
    }
}