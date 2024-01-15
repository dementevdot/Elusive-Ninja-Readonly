using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerMovment))]
    public class PlayerTrajectory : MonoBehaviour
    {
        [SerializeField] private int _maxPoints = 100;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _timeStep = 0.1f;
        [SerializeField] private Transform _dot;

        private PlayerInput _input;
        private PlayerMovment _movment;
        private PlayerHealth _health;
        private LineRenderer _lineRenderer;

        private Vector3[] _points;
        private Vector2 _lastDirection;
        private Vector3 _lastPosition;
        private Vector3 _startDotScale;

        public void Init(PlayerInput input, PlayerMovment movment, PlayerHealth health)
        {
            _input = input;
            _movment = movment;
            _health = health;

            _input.DirectionChanged += CheckInputStop;
            _health.HealthIsZero += TurnOffLineRenderer;
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _points = new Vector3[_maxPoints];
            _lineRenderer.positionCount = 0;
            _startDotScale = _dot.localScale;
        }

        private void OnDisable()
        {
            _input.DirectionChanged -= CheckInputStop;
            _health.HealthIsZero -= TurnOffLineRenderer;
        }

        private void FixedUpdate()
        {
            if (_input.Direction != Vector2.zero)
            {
                if (_input.Direction != _lastDirection || transform.position != _lastPosition)
                {
                    _lastDirection = _input.Direction;
                    var position = transform.position;
                    _lastPosition = position;

                    DrawTrajectory(position, _input.Direction * _movment.Power, Physics.gravity);
                }
            }
            else if (_lineRenderer.positionCount != 0)
            {
                _lineRenderer.positionCount = 0;

                return;
            }

            if (_lineRenderer.positionCount != 0)
            {
                Vector3 lastLinePosition = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);

                _dot.position = lastLinePosition;

                if (_dot.localScale == Vector3.zero)
                {
                    _dot.localScale = _startDotScale;
                }
            }
            else if (_dot.localScale != Vector3.zero)
            {
                _dot.localScale = Vector3.zero;
            }
        }

        public void TurnOffLineRenderer()
        {
            _lineRenderer.enabled = false;
            _dot.gameObject.SetActive(false);
        }

        private void CheckInputStop()
        {
            if (_input.Direction == Vector2.zero)
                _movment.SetJump(_lastDirection);
        }

        private void DrawTrajectory(Vector3 position, Vector3 velocity, Vector3 gravity)
        {
            int pointCount = 0;
            float distance = 0;
            float elapsedTime = 0;
            Vector3 currentPosition = position;
            Vector3 currentVelocity = velocity;

            _points[pointCount] = currentPosition;
            pointCount++;

            for (int i = 0; i < _maxPoints; i++)
            {
                Vector3 nextVelocity = currentVelocity + gravity * _timeStep;
                Vector3 nextPosition = currentPosition + (currentVelocity + nextVelocity) * (0.5f * _timeStep);

                elapsedTime += _timeStep;

                if (nextVelocity.y > _movment.VelocityToReach)
                    nextVelocity = new Vector3(
                        nextVelocity.x,
                        Mathf.Lerp(nextVelocity.y, 0, PlayerMovment.GetVelocityMultiplier(elapsedTime)), 
                        nextVelocity.z);

                distance += Vector2.Distance(currentPosition, nextPosition);

                if (distance > _maxDistance)
                {
                    SetPositions();
                    return;
                }

                if (Physics.Linecast(currentPosition, nextPosition, out RaycastHit hitInfo))
                {
                    nextPosition = hitInfo.point;

                    SetPoint(nextPosition);
                    SetPositions();

                    return;
                }

                SetPoint(nextPosition);

                if (pointCount >= _maxPoints)
                {
                    break;
                }

                currentPosition = nextPosition;
                currentVelocity = nextVelocity;
            }

            SetPositions();
            return;

            void SetPoint(Vector3 nextPosition)
            {
                _points[pointCount] = nextPosition;
                pointCount++;
            }

            void SetPositions()
            {
                _lineRenderer.positionCount = pointCount;
                _lineRenderer.SetPositions(_points);
            }
        }
    }
}