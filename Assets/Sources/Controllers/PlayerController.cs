using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    public sealed class PlayerController
    {
        private AnimationConfig _playerConfig;
        private SpriteAnimatorController _playerAnimator;
        private PlayerView _playerView;
        private Transform _playerTransform;

        private float _xAxisInput;
        private float _walkSpeed = 3.0f;
        private float _animationSpeed = 10.0f;
        private float _movingThreshold = 0.1f;
        private float _jumpForce = 7.0f;
        private float _jumpThreshold = 1.0f;
        private float _g = -9.8f;
        private float _groundLevel = 0.5f;
        private float _yVelocity;

        private int _turnRight = 1;
        private int _turnLeft = -1;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private bool _isJump;
        private bool _isMoving;
        
        public PlayerController(PlayerView player)
        {
            _playerView = player;
            _playerTransform = player.transform;
            _playerConfig = Resources.Load<AnimationConfig>("SpritePlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
        }
        
        public void Execute()
        {
            _playerAnimator.Execute();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Jump") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;

            if (_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                RunOrStay();
            }
            else
            {
                Jump();
            }
        }

        private void RunOrStay()
        {
            _playerAnimator.StartAnimation(_playerView.spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true,
                _animationSpeed);

            if (_isJump && _yVelocity <= 0)
            {
                _yVelocity = _jumpForce;
            }
            else if (_yVelocity < 0)
            {
                _yVelocity = 0;
                _playerTransform.position = new Vector3(_playerTransform.position.x, _groundLevel, _playerTransform.position.z);
            }
        }

        private void MoveTowards()
        {
            _playerTransform.position += Vector3.right * (Time.deltaTime * _walkSpeed * (_xAxisInput < 0 ? _turnLeft : _turnRight));
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        private bool IsGrounded()
        {
            return _playerTransform.position.y <= _groundLevel && _yVelocity <= 0;
        }

        private void Jump()
        {
            if (Mathf.Abs(_yVelocity) > _jumpThreshold)
            {
                _playerAnimator.StartAnimation(_playerView.spriteRenderer, AnimState.Jump, true, _animationSpeed);
            }
            _yVelocity += _g * Time.deltaTime;
            _playerTransform.position += Vector3.up * (Time.deltaTime * _yVelocity);
        }
    }
}
