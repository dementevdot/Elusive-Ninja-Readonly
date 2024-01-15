using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shared
{
    public abstract class ButtonsBinder : MonoBehaviour
    {
        private readonly Dictionary<Button, Action> _eventFuncPair = new();

        protected virtual void OnEnable()
        {
            foreach (var pair in _eventFuncPair)
                pair.Key.onClick.AddListener(pair.Value.Invoke);
        }

        protected virtual void OnDisable()
        {
            foreach (var pair in _eventFuncPair)
                pair.Key.onClick.RemoveListener(pair.Value.Invoke);
        }

        protected void AddBind(Button button, Action func) => _eventFuncPair.Add(button, func);
    }
}
