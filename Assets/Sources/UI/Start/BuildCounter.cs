using UnityEngine;
using UnityEngine.UI;

namespace UI.Start
{
    public class BuildCounter : MonoBehaviour
    {
        [SerializeField] private int _build;
        [SerializeField] private Text _text;

        private void OnValidate()
        {
            _build = PlayerPrefs.GetInt("build");
            _text.text = $"BUILD: {_build}";
        }

        #if UNITY_EDITOR
        private void Awake()
        {
            if (_build > PlayerPrefs.GetInt("build"))
                PlayerPrefs.SetInt("build", _build);

            PlayerPrefs.SetInt("build", PlayerPrefs.GetInt("build") + 1);
        }
        #endif
    }
}
