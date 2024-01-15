using System;
using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public event Action OnDie;

        public GameObject LastDamageSender { get; private set; }
        public bool Died { get; private set; }

        public void TakeDamage(uint _, GameObject sender)
        {
            if (Died == true) return;

            LastDamageSender = sender;
            OnDie?.Invoke();
            Die();
        }

        private void Die()
        {
            Died = true;
            StartCoroutine(DieCoroutine());
        }

        private IEnumerator DieCoroutine()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}