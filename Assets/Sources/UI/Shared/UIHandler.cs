using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UI.Shared
{
    public abstract class UIHandler : MonoBehaviour
    {
        private FieldInfo[] _screenFieldInfos;

        public static UIHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                throw new InvalidOperationException(nameof(Instance));

            var fieldInfos = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            List<FieldInfo> screenFieldInfos = new List<FieldInfo>();

            for (int i = 0; i < fieldInfos.Length; i++)
                if (fieldInfos[i].FieldType == typeof(GameObject))
                    screenFieldInfos.Add(fieldInfos[i]);

            _screenFieldInfos = screenFieldInfos.ToArray();
        }

        public void SetActiveScreen(string screenName)
        {
            uint activeScreenCount = 0;

            for (int i = 0; i < _screenFieldInfos.Length; i++)
            {
                var screen = (GameObject)_screenFieldInfos[i].GetValue(this);

                bool isScreen = _screenFieldInfos[i].Name.Substring(1) == screenName;

                if (isScreen == true)
                    screen.SetActive(true);
                else if (screen.activeSelf == true)
                    screen.SetActive(false);

                if (isScreen)
                    activeScreenCount++;
            }

            if (activeScreenCount != 1)
                throw new InvalidOperationException(screenName);
        }

        public void SetAllScreensOff()
        {
            for (int i = 0; i < _screenFieldInfos.Length; i++)
            {
                var screen = (GameObject)_screenFieldInfos[i].GetValue(this);

                if (screen.activeSelf == true)
                    screen.SetActive(false);
            }
        }
    }
}
