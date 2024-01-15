using System;
using UnityEngine;

namespace Game
{
    public class ColliderRouter : MonoBehaviour
    {
        public event Action<Collision> CollisionEnter;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter?.Invoke(collision);
        }
    }
}
