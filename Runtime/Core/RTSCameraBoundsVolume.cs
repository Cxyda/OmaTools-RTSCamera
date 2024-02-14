using UnityEngine;

namespace Plugins.O.M.A.Games.RTSCamera.Runtime.Core
{
    [ExecuteAlways]
    [RequireComponent(typeof(Collider))]
    public class RTSCameraBoundsVolume : MonoBehaviour
    {
        [SerializeField] private Collider _boundsCollider;

        private void OnValidate()
        {
            InitializeCollider();
        }

        private void Awake()
        {
            InitializeCollider();
        }

        private void InitializeCollider()
        {
            if (_boundsCollider)
            {
                _boundsCollider.isTrigger = true;
                return;
            }

            _boundsCollider = gameObject.GetComponent<Collider>();
            if (!_boundsCollider)
            {
                _boundsCollider = gameObject.AddComponent<BoxCollider>();
            }
            _boundsCollider.isTrigger = true;
        }

        public bool IsTargetWithinBounds(Vector3 target)
        {
            return _boundsCollider.bounds.Contains(target);
        }

        public Vector3 GetClosestPointOnBounds(Vector3 smoothedTargetPosition)
        {
            return _boundsCollider.ClosestPointOnBounds(smoothedTargetPosition);
        }
    }
}