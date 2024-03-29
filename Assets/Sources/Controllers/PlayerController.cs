using UnityEngine;

namespace Platformer
{
    public sealed class PlayerController
    {
        private AnimationConfig _playerConfig;
        private SpriteAnimatorController _playerAnimator;
        private ContactPooler _contactPooler;
        private LevelObjectView _playerView;
        private Transform _playerTransform;
        private Rigidbody2D _playerRigidbody;

        private int _health = 100;
        private float _walkSpeed = 130f;
        private float _animationSpeed = 14.0f;
        private float _movingThreshold = 0.1f;
        private float _jumpForce = 8.0f;
        private float _jumpThreshold = 1.5f;
        private float _yVelocity = 0;
        private float _xVelocity = 0;
        private float _xAxisInput;
        

        private int _turnRight = 1;
        private int _turnLeft = -1;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private bool _isJump;
        private bool _isMoving;
        
        public PlayerController(InteractiveObjectView playerView)
        {
            _playerView = playerView;
            _playerTransform = playerView.trans;
            _playerRigidbody = playerView.rigidBody;
            _playerConfig = Resources.Load<AnimationConfig>("SpritePlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _contactPooler = new ContactPooler(playerView.coll);

            playerView.TakeDamage += TakeDamageFromBullet;
        }
        
        public void Execute()
        {
            if (_health <= 0)
            {
                _health = 0;
                _playerView.spriteRenderer.enabled = false;
            }
            
            _playerAnimator.Execute();
            _contactPooler.Excute();
            
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Jump") > 0;
            _yVelocity = _playerRigidbody.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            
            MoveTowards();
            Jump();
        }

        private void MoveTowards()
        { 
            _playerAnimator.StartAnimation(_playerView.spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true,
                _animationSpeed);
            
            if (_isMoving)
            {
                _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? _turnLeft : _turnRight);
                _playerRigidbody.velocity = new Vector2(_xVelocity, _yVelocity);
                _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;    
            }
            else
            {
                _xVelocity = 0;
                _playerRigidbody.velocity = new Vector2(_xVelocity, _yVelocity);
            }
        }

        private void Jump()
        {
            if (_contactPooler.IsGrounded)
            {
                if (_isJump && _yVelocity <= _jumpThreshold)
                {
                    _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _playerAnimator.StartAnimation(_playerView.spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }

        private void TakeDamageFromBullet(BulletView bullet)
        {
            _health -= bullet.DamagePoint;
        }
    }
}
