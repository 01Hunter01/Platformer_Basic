using UnityEngine;

namespace Plarformer
{
    
    public class EnemyController
    {
        private int _enemyLayerMask = 1 << 7;
        private LevelObjectView _enemyView;
        private Rigidbody2D _enemyRigidbody;
        private Transform _enemyTransform; 
        
        private RaycastHit2D _hit2D;
        private Vector2 _origin;
        private Vector2 _direction;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private int _turnRight = 1;
        private int _turnLeft = -1;
        private float _enemySpeed = 30f;
        private float _rayDistance = 1f;
        private float _yVelocity;
        private float _xVelocity;
        private bool _faceRight = true;

        public EnemyController(LevelObjectView enemyView)
        {
            _enemyRigidbody = enemyView.rigidBody;
            _enemyTransform = enemyView.trans;
        }

        public void Execute()
        {
            _yVelocity = _enemyRigidbody.velocity.y;
            _origin = new Vector2(_enemyTransform.position.x, _enemyTransform.position.y);
            _direction = new Vector2(_faceRight ? _turnRight : _turnLeft, -1);
            _hit2D = Physics2D.Raycast(_origin, _direction,_rayDistance, _enemyLayerMask);

            MoveOnPlatform();
        }

        private void MoveOnPlatform()
        {
            if (_hit2D.collider && _faceRight)
            {
                Debug.DrawRay(_origin, _direction * _hit2D.distance, Color.red);
                MoveEnemy();
            }
            else if (_hit2D.collider && !_faceRight)
            {
                Debug.DrawRay(_origin, _direction * _hit2D.distance, Color.red);
                MoveEnemy();
            }
            else
            {
                Debug.DrawRay(_origin, _direction * 100, Color.white);
                _faceRight = !_faceRight;
                Flip();
            }
        }

        private void MoveEnemy()
        {
            _xVelocity = Time.fixedDeltaTime * _enemySpeed * (_faceRight ? _turnRight : _turnLeft);
            _enemyRigidbody.velocity = new Vector2(_xVelocity, _yVelocity);
        }

        private void Flip()
        {
            _enemyTransform.localScale = _faceRight ? _rightScale : _leftScale;
        }
    }
}
