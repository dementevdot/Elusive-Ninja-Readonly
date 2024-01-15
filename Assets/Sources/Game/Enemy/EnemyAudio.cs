using System;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(EnemyCombat))]
    public class EnemyAudio : AudioRouter
    {
        [SerializeField] private AudioClip _onShoot;

        private EnemyCombat _combat;

        private event Action OnShootPlay;

        protected override void OnEnable()
        {
            base.OnEnable();

            _combat = GetComponent<EnemyCombat>();
            OnShootPlay = () => PlayAudio(_onShoot);
            _combat.OnShoot += OnShootPlay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _combat.OnShoot += OnShootPlay;
        }
    }
}
