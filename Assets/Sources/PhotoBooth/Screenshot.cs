using UnityEngine;

namespace PhotoBooth
{
    public class Screenshot : MonoBehaviour
    {
        [SerializeField] private bool _takeScreenshot;
        [SerializeField] private string _path;

        private void OnValidate()
        {
            if (_takeScreenshot == false) return;

            ScreenCapture.CaptureScreenshot(_path);
            _takeScreenshot = false;
        }
    }
}
