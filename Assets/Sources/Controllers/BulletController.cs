using UnityEngine;

namespace Platformer
{
    public sealed class BulletController
    {
        private Vector3 _velocity;
        private BulletView _bulletView;
        private float _angle;
        private Vector3 _axis;

        public BulletController(BulletView bulletView)
        {
            _bulletView = bulletView;
            Active(false);
        }

        public void Active(bool value)
        {
            _bulletView.gameObject.SetActive(value);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            _angle = Vector3.Angle(Vector3.down, _velocity);
            _axis = Vector3.Cross(Vector3.down, _velocity);
            _bulletView.trans.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void ThrowBullet(Vector3 position, Vector3 velocity)
        {
            _bulletView.trans.position = position;
            SetVelocity(_velocity);
            _bulletView.rigidBody.velocity = Vector2.zero;
            _bulletView.rigidBody.angularVelocity = 0;
            Active(true);
            
            _bulletView.rigidBody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}
