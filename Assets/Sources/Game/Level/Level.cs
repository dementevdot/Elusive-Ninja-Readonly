using System;
using UnityEngine;

namespace Game.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _startEndPoint;
        [SerializeField] private Transform _endStartPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _endStartRadius;
        [SerializeField] private MeshRenderer _innerWalls;
        [SerializeField] private MeshRenderer _outsideWalls;

        public Vector3 StartPosition => _startPoint.position;

        public Vector3 StartEndPosition => _startEndPoint.position;

        public Vector3 EndStartPosition => _endStartPoint.position;

        public Vector3 EndPosition => _endPoint.position;

        public float EndStartRadius => _endStartRadius;

        private void Awake()
        {
            if (_startPoint == null || _startEndPoint == null ||
                _endStartPoint == null || _endPoint == null)
                throw new NullReferenceException();
        }

        public void SetLevelMaterial(Material inner, Material outside)
        {
            _innerWalls.material = inner;
            _outsideWalls.material = outside;
        }
    }
}