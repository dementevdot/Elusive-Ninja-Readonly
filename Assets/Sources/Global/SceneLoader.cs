using UnityEngine.SceneManagement;

namespace Global
{
    public static class SceneLoader
    {
        public const string Menu = "Menu";
        public const string Game = "Game";

        public static void LoadScene(string scene) => SceneManager.LoadScene(scene);
    }
}