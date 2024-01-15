using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class SkinCell : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Image _picture;
        [SerializeField] private Image _selected;
        [SerializeField] private GameObject _priceTag;
        [SerializeField] private Text _priceTagText;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;

        private Skin _skin;
        private uint _price;
        private bool _isBuyed;

        public void Init(Skin skin, uint price, Sprite picture)
        {
            _skin = skin;
            _price = price;
            _priceTagText.text = _price.ToString();
            _picture.sprite = picture;

            _selectButton.onClick.AddListener(OnClick);
            PlayerPrefsService.CurrentSkin.ValueChanged += SetView;

            SetView(PlayerPrefsService.CurrentSkin.Value);
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(OnClick);
            PlayerPrefsService.CurrentSkin.ValueChanged -= SetView;
        }

        private void SetView(Skin skin)
        {
            bool isCurrentSkin = skin == _skin;
            bool isSkinBuyed = isCurrentSkin || PlayerPrefsService.UnlockedSkins.Value.Contains(_skin);

            _selected.color = isCurrentSkin ? _selectedColor : _unselectedColor;
            _priceTag.SetActive(isSkinBuyed == false);
            _isBuyed = isSkinBuyed;
        }

        private void OnClick()
        {
            if (_skin == PlayerPrefsService.CurrentSkin.Value) return;

            if (_isBuyed == false)
            {
                if (PlayerPrefsService.Coins.Value < _price) return;

                PlayerPrefsService.Coins.Value -= _price;
                PlayerPrefsService.UnlockedSkins.Value.Add(_skin);
                PlayerPrefsService.UnlockedSkins.Value = PlayerPrefsService.UnlockedSkins.Value;
                _isBuyed = true;
            }

            PlayerPrefsService.CurrentSkin.Value = _skin;
            SetView(PlayerPrefsService.CurrentSkin.Value);
        }
    }
}
