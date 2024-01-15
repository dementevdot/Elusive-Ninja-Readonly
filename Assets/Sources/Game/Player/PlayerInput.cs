using System;
using UnityEngine;

namespace Game.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        public event Action DirectionChanged;

        public Vector2 Direction { get; private set; }

        private void Awake()
        {
            if (_joystick == null)
                throw new NullReferenceException(nameof(_joystick));
        }

        private void Update()
        {
            if (_joystick.Direction == Direction) return;

            Direction = _joystick.Direction;
            DirectionChanged?.Invoke();
        }
    }
}
