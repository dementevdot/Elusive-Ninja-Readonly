using System;
using UnityEngine;

namespace Shared
{
    public abstract class Initializable : MonoBehaviour
    {
        public Action Inited;

        public bool IsInited { get; private set; }

        private void Awake()
        {
            Inited += () => IsInited = true;
        }
    }
}