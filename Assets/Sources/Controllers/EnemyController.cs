using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    
    public class EnemyController
    {
        private int _enemyLayerMask = 1 << 7;
        private int _playerLayerMask = 1 << 3;
        private Rigidbody2D _enemyRigidbody;
        private Transform _enemyTransform;
        private Transform _playerTransform;
        
        private RaycastHit2D _hitPlatform;
        private RaycastHit2D _hitPlayer;
        private Vector2 _origin;
        private Vector2 _directionEnemy;
        private Vector2 _directionPlayer = Vector2.right;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private int _turnRight = 1;
        private int _turnLeft = -1;
        private float _enemySpeed = 50f;
        private float _rayDistanceToPlatform = 1f;
        private float _rayDistanceToPlayer = 2f;
        private float _yVelocity;
        private float _xVelocity;
        private bool _faceRight = true;

        public EnemyController(LevelObjectView enemyView, LevelObjectView playerView)
        {
            _enemyRigidbody = enemyView.rigidBody;
            _enemyTransform = enemyView.trans;
            _playerTransform = playerView.trans;
        }

        public void Execute()
        {
            _yVelocity = _enemyRigidbody.velocity.y;
            _origin = new Vector2(_enemyTransform.position.x, _enemyTransform.position.y);
            _directionEnemy = new Vector2(_faceRight ? _turnRight : _turnLeft, -1);
            _hitPlatform = Physics2D.Raycast(_origin, _directionEnemy, _rayDistanceToPlatform, _enemyLayerMask);
            _hitPlayer = Physics2D.Raycast(_origin, _directionPlayer, _rayDistanceToPlayer, _playerLayerMask);

            if (_hitPlayer && _hitPlatform.distance <= _rayDistanceToPlayer)
            {
                Debug.DrawRay(_origin, _directionPlayer * _hitPlayer.distance, Color.blue);
                var offset = _enemyTransform.position - _playerTransform.position;
                _enemyTransform.position = _playerTransform.transform.position + offset;
            }
            else
            {
                Debug.DrawRay(_origin, _directionPlayer * _rayDistanceToPlayer, Color.green);
                MoveOnPlatform();
            }
            
        }

        private void MoveOnPlatform()
        {
            if (_hitPlatform.collider && _faceRight)
            {
                Debug.DrawRay(_origin, _directionEnemy * _hitPlatform.distance, Color.red);
                MoveEnemy();
            }
            else if (_hitPlatform.collider && !_faceRight)
            {
                Debug.DrawRay(_origin, _directionEnemy * _hitPlatform.distance, Color.red);
                MoveEnemy();
            }
            else
            {
                Debug.DrawRay(_origin, _directionEnemy * 100, Color.white);
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
