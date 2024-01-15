using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private SO.Skin[] _skins;

        public static Dictionary<Skin, SO.Skin> Skins { get; private set; }

        private void Awake()
        {
            if (_skins.Length != Enum.GetValues(typeof(Skin)).Length)
                throw new InvalidOperationException(nameof(Skins));

            if (Skins == null)
            {
                Skins = new Dictionary<Skin, SO.Skin>();

                foreach (var skin in _skins)
                {
                    Skins.Add(skin.Name, skin);
                }
            }
            else
            {
                throw new InvalidOperationException(nameof(Skins));
            }
        }
    }
}
