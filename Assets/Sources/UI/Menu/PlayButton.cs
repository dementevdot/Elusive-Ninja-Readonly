using UnityEngine;
using UnityEngine.UI;
using Global;
using UI.Shared;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : ButtonsBinder
    {
        private Button _playButton;

        private void Awake()
        {
            _playButton = GetComponent<Button>();
            AddBind(_playButton, () => SceneLoader.LoadScene(SceneLoader.Game));
        }
    }
}
