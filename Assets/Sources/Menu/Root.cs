using Shared;
using UI.Shared;
using UnityEngine;

namespace Menu
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ScreenRatio _screenRatio;
        [SerializeField] private OrientationViewHandler[] _orientationViewHandlers;
        [SerializeField] private ScaleByRatio[] _scaleByRatios;

        private void Awake()
        {
            foreach (var orientationViewHandler in _orientationViewHandlers)
                orientationViewHandler.Init(_screenRatio);

            foreach (var scaleByRatio in _scaleByRatios)
                scaleByRatio.Init(_screenRatio);
        }
    }
}
