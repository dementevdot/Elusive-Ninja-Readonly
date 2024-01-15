namespace Game.Camera
{
    public class Dimension
    {
        private readonly bool _isFollow;
        private readonly float _offset;
        private float _value;

        public Dimension(float value, bool isFollow, float offset)
        {
            _value = value;
            _isFollow = isFollow;
            _offset = offset;
        }

        public float Value => _value;

        public void SetValue(float value)
        {
            if (_isFollow) _value = value + _offset;
        }
    }
}