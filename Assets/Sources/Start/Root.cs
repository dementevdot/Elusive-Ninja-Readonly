using Shared;
using UnityEngine;

namespace Start
{
    public class Root : Initializable
    {
        [SerializeField] private Initializable[] _initializables;

        private uint _initedCount;

        private void Awake()
        {
            foreach (var initializable in _initializables)
                initializable.Inited += SetInitedCount;
        }

        private void SetInitedCount()
        {
            _initedCount++;

            if (_initedCount >= _initializables.Length)
                Inited?.Invoke();
        }
    }
}
