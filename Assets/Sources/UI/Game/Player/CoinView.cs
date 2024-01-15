using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Player
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private void OnEnable()
        {
            UpdateText(PlayerPrefsService.Coins.Value);
            PlayerPrefsService.Coins.ValueChanged += UpdateText;
        }

        private void OnDisable()
        {
            PlayerPrefsService.Coins.ValueChanged -= UpdateText;
        }

        private void UpdateText(uint coins)
        {
            _text.text = coins.ToString();
        }
    }
}
