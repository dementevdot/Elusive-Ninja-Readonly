using System;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudio : AudioRouter
    {
        [SerializeField] private AudioClip _onJump;
        [SerializeField] private AudioClip _onPunch;

        private PlayerCombat _combat;
        private PlayerMovment _movment;

        private Action OnJumpPlay;
        private Action OnPunchPlay;

        public void Init(PlayerCombat combat, PlayerMovment movment)
        {
            OnJumpPlay = () => PlayAudio(_onJump);
            OnPunchPlay = () => PlayAudio(_onPunch);

            _movment = movment;
            _combat = combat;

            _movment.OnJump += OnJumpPlay;
            _combat.OnPunch += OnPunchPlay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _movment.OnJump -= OnJumpPlay;
            _combat.OnPunch -= OnPunchPlay;
        }
    }
}
