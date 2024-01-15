using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Create new stage")]
    public class Stage : ScriptableObject
    {
        public const uint MaxLevelCount = 4;

        [SerializeField] private GameObject[] _levels;
        [SerializeField] private Material _innerWalls;
        [SerializeField] private Material _outsideWalls;
        [SerializeField] private Material _skybox;

        public GameObject[] Levels => _levels;
        public Material InnerMaterial => _innerWalls;
        public Material OutsideMaterial => _outsideWalls;
        public Material Skybox => _skybox;
    }
}
