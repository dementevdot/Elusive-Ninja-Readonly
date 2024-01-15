using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private GameObject _leaderboardCellPrefab;
        [SerializeField] private GameObject _nonAuth;
        [SerializeField] private Button _authButton;
        [SerializeField] private Transform _leaderboardCellParent;
        [SerializeField] private Button _closeButton;

        private void Awake()
        {
            #if !UNITY_EDITOR
                if (PlayerAccount.IsAuthorized == true)
                {
                    _authButton.gameObject.SetActive(false);
                    SetLeaderboard();

                    if (PlayerAccount.HasPersonalProfileDataPermission == false)
                    {
                        PlayerAccount.RequestPersonalProfileDataPermission();
                    }
                }
                else
                {
                    _authButton.onClick.AddListener(Auth);
                }
            #endif

            _closeButton.onClick.AddListener(() => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.MainUI));

            #if UNITY_EDITOR
                _nonAuth.SetActive(false);
                _authButton.gameObject.SetActive(false);
                SetFakeLeaderboard();
            #endif
        }

        private void Auth()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                PlayerAccount.Authorize(() => PlayerAccount.RequestPersonalProfileDataPermission());
                MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.MainUI);
            }
            else
            {
                SetLeaderboard();
            }
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(() => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.MainUI));
        }

        private void SetLeaderboard()
        {
            _nonAuth.SetActive(false);
            _authButton.gameObject.SetActive(false);

            void OnSuccessCallback(LeaderboardGetEntriesResponse response)
            {
                foreach (var entry in response.entries)
                    Instantiate(_leaderboardCellPrefab, _leaderboardCellParent)
                        .GetComponent<LeaderboardCell>()
                        .Init(entry.rank, entry.player.publicName, entry.score);
            }

            Agava.YandexGames.Leaderboard.GetEntries(
                "enemiesKilled", OnSuccessCallback, topPlayersCount: 3, competingPlayersCount: 3, includeSelf: true);
        }

        private void SetFakeLeaderboard()
        {
            for (int i = 1; i < 6; i++)
            {
                Instantiate(_leaderboardCellPrefab, _leaderboardCellParent)
                    .GetComponent<LeaderboardCell>()
                    .Init(i, i.ToString(), i);
            }
        }
    }
}
