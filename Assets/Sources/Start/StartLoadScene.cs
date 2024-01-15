using Agava.YandexGames;
using Global;
using UnityEngine;

namespace Start
{
    public class StartLoadScene : MonoBehaviour
    {
        [SerializeField] private Root _root;

        private void Awake()
        {
            _root.Inited += () =>
            {
                SceneLoader.LoadScene(SceneLoader.Menu);
                #if !UNITY_EDITOR
                YandexGamesSdk.GameReady();
                #endif
            };
        }
    }
}