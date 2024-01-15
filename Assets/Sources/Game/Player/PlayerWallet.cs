using Global;
using UnityEngine;

namespace Game.Player
{
    public class PlayerWallet : MonoBehaviour
    {
        public static void AddCoins(uint coins)
        {
            PlayerPrefsService.Coins.Value += coins;
        }

        public static bool TryTakeCoins(uint coins)
        {
            if (coins > PlayerPrefsService.Coins.Value)
                return false;

            PlayerPrefsService.Coins.Value -= coins;

            return true;
        }
    }
}