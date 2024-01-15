using Shared;
using UnityEngine;

namespace Game.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraRatioFOV : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curveFOV;
        [SerializeField] private float _minFOV;
        [SerializeField] private float _maxFOV;

        private UnityEngine.Camera _camera;
        private ScreenRatio _screenRatio;

        public void Init(ScreenRatio screenRatio)
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _screenRatio = screenRatio;
            _screenRatio.RatioChanged += SetFOV;
            SetFOV();
        }

        private void OnDestroy()
        {
            _screenRatio.RatioChanged -= SetFOV;
        }

        private void SetFOV()
        {
            var ratio = _screenRatio.Current;
            const int minRatioValue = 1;
            _camera.fieldOfView = Mathf.Lerp(_minFOV, _maxFOV, _curveFOV.Evaluate(ratio.y - minRatioValue));
        }
    }
}
