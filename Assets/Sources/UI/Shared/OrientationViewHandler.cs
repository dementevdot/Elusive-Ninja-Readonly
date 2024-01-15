using System;
using Shared;
using UnityEngine;

namespace UI.Shared
{
    [RequireComponent(typeof(RectTransform))]
    public class OrientationViewHandler : MonoBehaviour
    {
        [SerializeField] private OrientationTransform _landscapeTransform;
        [SerializeField] private OrientationTransform _portraitTransform;
        [Space(10)]
        [SerializeField] private Vector2 _portraitRatio = new (3, 4);

        private RectTransform _rectTransform;
        private ScreenRatio _screenRatio;

        public void Init(ScreenRatio screenRatio)
        {
            _rectTransform = GetComponent<RectTransform>();
            _screenRatio = screenRatio;
            _screenRatio.RatioChanged += SetTransform;

            SetTransform();
        }

        private void OnDestroy()
        {
            _screenRatio.RatioChanged -= SetTransform;
        }

        private void SetTransform()
        {
            var currentRatio = _screenRatio.Current;
            var portraitRatio = ScreenRatio.GetRatio(_portraitRatio.x, _portraitRatio.y);

            OrientationTransform orientationTransform;

            if (portraitRatio.x > portraitRatio.y)
                orientationTransform = currentRatio.x < portraitRatio.x ? _portraitTransform : _landscapeTransform;
            else
                orientationTransform = currentRatio.y > portraitRatio.y ? _portraitTransform : _landscapeTransform;

            _rectTransform.anchoredPosition3D = orientationTransform.Position;
            _rectTransform.rotation = Quaternion.Euler(orientationTransform.Rotation);
            _rectTransform.localScale = orientationTransform.Scale;
        }

        [Serializable] private class OrientationTransform
        {
            [SerializeField] private Vector3 _anchoredPosition;
            [SerializeField] private Vector3 _rotation;
            [SerializeField] private Vector3 _scale = Vector3.one;

            public Vector3 Position => _anchoredPosition;
            public Vector3 Rotation => _rotation;
            public Vector3 Scale => _scale;
        }
    }
}
