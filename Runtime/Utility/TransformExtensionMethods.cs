using System;
using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Utility
{
    public static class TransformExtensionMethods
    {
        public static Transform FindChildWithTag(this Transform currentTransform, string cameraTargetTag)
        {
            for (var i = 0; i < currentTransform.childCount; i++)
            {
                var child = currentTransform.GetChild(i);
                if (child.tag.Equals(cameraTargetTag))
                {
                    return child;
                }
            }

            throw new Exception($"Unable to find child with '{cameraTargetTag}' tag");
        }
    }
}