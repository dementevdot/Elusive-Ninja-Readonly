using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Skin", menuName = "Skin/Create new skin")]
    public class Skin : ScriptableObject
    {
        [SerializeField] private Global.Skin _name;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private uint _cost;
        [SerializeField] private Sprite _picture;

        public Global.Skin Name => _name;
        public GameObject Prefab => _prefab;
        public uint Cost => _cost;
        public Sprite Picture => _picture;
    }
}
