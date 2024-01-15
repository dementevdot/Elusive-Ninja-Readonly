using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class TutorialPointerHandler : MonoBehaviour
    {
        [SerializeField] private Image _pointerImage;
        [SerializeField] private Sprite _desktopPointer;
        [SerializeField] private Sprite _mobilePointer;

        #if !UNITY_EDITOR
        private void Awake() => _pointerImage.sprite = Device.IsMobile ? _mobilePointer : _desktopPointer;
        #endif
    }
}
