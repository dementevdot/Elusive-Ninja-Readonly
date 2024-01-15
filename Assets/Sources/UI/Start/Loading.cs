using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Start
{
    [RequireComponent(typeof(Text))]
    public class Loading : MonoBehaviour
    {
        [SerializeField] private float _delay;

        private Text _text;
        private Coroutine _loadingCoroutine;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _loadingCoroutine = StartCoroutine(LoadingCoroutine());
        }

        private void OnDestroy()
        {
            StopCoroutine(_loadingCoroutine);
        }

        private IEnumerator LoadingCoroutine()
        {
            var delay = new WaitForSeconds(_delay);
            _text.text = string.Empty;

            while (true)
            {
                _text.text += ".";

                if (_text.text.Length >= 4)
                    _text.text = string.Empty;

                yield return delay;
            }
        }
    }
}
