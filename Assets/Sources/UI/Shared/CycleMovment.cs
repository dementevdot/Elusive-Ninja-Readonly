using System.Collections;
using UnityEngine;

namespace UI.Shared
{
    public class CycleMovment : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private float _movmentSpeed;
        [SerializeField] private float _delay;
        [SerializeField] private AnimationCurve _movmentCurve;

        private Coroutine _movmentCorotine;

        private void OnEnable()
        {
            _movmentCorotine = StartCoroutine(PointerMovment());
        }

        private void OnDisable()
        {
            StopCoroutine(_movmentCorotine);
        }

        private IEnumerator PointerMovment()
        {
            float elapsedTime = 0;
            var delay = new WaitForSeconds(_delay);

            while (true)
            {
                while (elapsedTime < 1)
                {
                    float distance = Vector2.Distance(_startPosition, _endPosition) / 100;
                    _transform.anchoredPosition = Vector3.Lerp(_startPosition, _endPosition, _movmentCurve.Evaluate(elapsedTime));
                    elapsedTime += Time.deltaTime * _movmentSpeed / distance;
                    yield return new WaitForEndOfFrame();
                }

                elapsedTime = 0;

                yield return delay;
            }
        }
    }
}
