using Shared;
using UnityEngine;

namespace UI.Shared
{
    [RequireComponent(typeof(RectTransform))]
    public class ScaleByRatio : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _scaleCurve;

        private RectTransform _rectTransform;
        private Vector3 _startScale;
        private ScreenRatio _screenRatio;

        public void Init(ScreenRatio screenRatio)
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenRatio = screenRatio;
            _screenRatio.RatioChanged += OnRatioChanged;
            _startScale = _rectTransform.localScale;

            OnRatioChanged();
        }

        private void OnDestroy()
        {
            _screenRatio.RatioChanged -= OnRatioChanged;
        }

        private void OnRatioChanged()
        {
            Vector2 currentRatio = _screenRatio.Current;
            float multiplier = currentRatio.x > currentRatio.y ? _scaleCurve.Evaluate(currentRatio.x - 1) : _scaleCurve.Evaluate(-currentRatio.y + 1);
            _rectTransform.localScale = _startScale * multiplier;
        }
    }
}
