using UnityEngine;

namespace Plarformer
{
    public sealed class PlayerController
    {
        private AnimationConfig _playerConfig;
        private SpriteAnimatorController _playerAnimator;
        private ContactPooler _contactPooler;
        private LevelObjectView _playerViewView;
        private Transform _playerTransform;
        private Rigidbody2D _playerRigidbody;

        private float _xAxisInput;
        private float _walkSpeed = 130f;
        private float _animationSpeed = 10.0f;
        private float _movingThreshold = 0.1f;
        private float _jumpForce = 6.0f;
        private float _jumpThreshold = 1.0f;
        private float _yVelocity = 0;
        private float _xVelocity = 0;
        

        private int _turnRight = 1;
        private int _turnLeft = -1;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        
        private bool _isJump;
        private bool _isMoving;
        
        public PlayerController(LevelObjectView playerView)
        {
            _playerViewView = playerView;
            _playerTransform = playerView.trans;
            _playerRigidbody = playerView.rigidBody;
            _playerConfig = Resources.Load<AnimationConfig>("SpritePlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _contactPooler = new ContactPooler(playerView.coll);
        }
        
        public void Execute()
        {
            _playerAnimator.Execute();
            _contactPooler.Excute();
            
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Jump") > 0;
            _yVelocity = _playerRigidbody.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            _playerAnimator.StartAnimation(_playerViewView.spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true,
                _animationSpeed);
            
            MoveTowards();
            Jump();
        }

        private void MoveTowards()
        { 
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
                    _playerAnimator.StartAnimation(_playerViewView.spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
            }
        }
    }
}
