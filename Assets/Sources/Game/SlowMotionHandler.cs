using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class SlowMotionHandler : MonoBehaviour
    {
        private Coroutine _timeScaleLerpCoroutine;

        private float? _pauseTimeScale;
        private float _startScale;
        private float _endScale;
        private float _smooth;
        private float _elapsedTime;

        private void OnDisable()
        {
            Stop();
        }

        public void SetStart(float startSpeed, float endSpeed, float smooth)
        {
            if (_timeScaleLerpCoroutine != null)
                StopCoroutine(_timeScaleLerpCoroutine);

            _timeScaleLerpCoroutine = StartCoroutine(TimeScaleLerp(startSpeed, endSpeed, smooth, true));
        }

        public void Stop()
        {
            if (_timeScaleLerpCoroutine != null)
                StopCoroutine(_timeScaleLerpCoroutine);

            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        public void StopSmooth() => SetStart(Time.timeScale, 1f, 5);

        public void Pause(bool isPause)
        {
            if (_pauseTimeScale == null && isPause == true)
            {
                _pauseTimeScale = Time.timeScale;

                Stop();

                Time.timeScale = 0;

                return;
            }

            if (_pauseTimeScale != null && isPause == false)
            {
                Stop();

                if (_timeScaleLerpCoroutine != null)
                    StopCoroutine(_timeScaleLerpCoroutine);

                Time.timeScale = (float)_pauseTimeScale;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;

                _timeScaleLerpCoroutine = StartCoroutine(TimeScaleLerp(0, 0, 0, false));

                _pauseTimeScale = null;

                return;
            }

            throw new InvalidOperationException();
        }

        private IEnumerator TimeScaleLerp(float startScale, float endScale, float smooth, bool isNew)
        {
            if (isNew == true)
            {
                _startScale = startScale;
                _endScale = endScale;
                _smooth = smooth;
                _elapsedTime = 0f;
            }

            while (_elapsedTime < 1)
            {
                Time.timeScale = Mathf.Lerp(_startScale, _endScale, _elapsedTime);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                _elapsedTime += Time.deltaTime * _smooth;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
