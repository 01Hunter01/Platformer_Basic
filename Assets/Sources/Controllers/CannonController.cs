using UnityEngine;

namespace Plarformer
{
    public sealed class CannonController
    {
        private Transform _muzzleT;
        private Transform _targetT;

        private Vector3 _direction;
        private Vector3 _axis;
        private float _angle;

        public CannonController(Transform muzzle, Transform target)
        {
            _muzzleT = muzzle;
            _targetT = target;
        }

        public void Execute()
        {
            _direction = _targetT.position - _muzzleT.position;
            _angle = Vector3.Angle(Vector3.down, _direction);
            _axis = Vector3.Cross(Vector3.down, _direction);
            
            _muzzleT.rotation = Quaternion.AngleAxis(_angle, _axis);

        }
        
    }
}
