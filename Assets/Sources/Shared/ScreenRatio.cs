using System;
using System.Collections;
using UnityEngine;

namespace Shared
{
    public class ScreenRatio : MonoBehaviour
    {
        private Coroutine _ratioCheckCoroutine;

        public event Action RatioChanged;

        public Vector2 Current { get; private set; }

        private void Awake()
        {
            _ratioCheckCoroutine = StartCoroutine(RatioCheck());
            Current = GetScreenRatio();
        }

        private void OnDestroy()
        {
            StopCoroutine(_ratioCheckCoroutine);
        }

        public static Vector2 GetRatio(float width, float height)
        {
            return new Vector2(width > height ? width / height : 1, height > width ? height / width : 1);
        }

        private static Vector2 GetScreenRatio()
        {
            float width = Screen.width;
            float height = Screen.height;

            return new Vector2(width > height ? width / height : 1, height > width ? height / width : 1);
        }

        private IEnumerator RatioCheck()
        {
            var delay = new WaitForSecondsRealtime(0.4f);

            while (true)
            {
                Vector2 ratio = GetScreenRatio();

                if (Current == ratio)
                {
                    yield return delay;
                    continue;
                }

                Current = ratio;
                RatioChanged?.Invoke();
            }
        }
    }
}
