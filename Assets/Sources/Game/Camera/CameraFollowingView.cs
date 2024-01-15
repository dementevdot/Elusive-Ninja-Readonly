using UnityEngine;

namespace Game.Camera
{
    public class CameraFollowingView : MonoBehaviour
    {
        [SerializeField] private Transform _followedTransform;

        [SerializeField] private bool _isFollowed;
        [SerializeField] private bool _isFollowedX;
        [SerializeField] private bool _isFollowedY;
        [SerializeField] private bool _isFollowedZ;

        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;

        private Dimension x;
        private Dimension y;
        private Dimension z;

        public void SetFollowing(bool isFollowed)
        {
            _isFollowed = isFollowed;
        }

        public void SetPosition(Vector3 position)
        {
            x.SetValue(position.x);
            y.SetValue(position.y);
            z.SetValue(position.z);

            transform.position = new Vector3(x.Value, y.Value, z.Value);
        }

        private void Awake()
        {
            var position = transform.position;
            x = new Dimension(position.x, _isFollowedX, _offsetX);
            y = new Dimension(position.y, _isFollowedY, _offsetY);
            z = new Dimension(position.z, _isFollowedZ, _offsetZ);
        }

        private void LateUpdate()
        {
            if (_isFollowed == false)
                return;

            var position = _followedTransform.position;
            x.SetValue(position.x);
            y.SetValue(position.y);
            z.SetValue(position.z);

            transform.position = new Vector3(x.Value, y.Value, z.Value);
        }
    }
}
