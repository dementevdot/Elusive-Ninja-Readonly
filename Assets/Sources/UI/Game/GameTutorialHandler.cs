using Game.Player;
using UnityEngine;

namespace UI.Game
{
    public class GameTutorialHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovment _playerMovment;

        private void Awake()
        {
            _playerMovment.OnJump += OnJump;
        }

        private void OnDestroy()
        {
            _playerMovment.OnJump -= OnJump;
        }

        private void OnJump()
        {
            Destroy(gameObject);
        }
    }
}
