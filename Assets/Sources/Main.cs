using System;
using UnityEngine;

namespace Plarformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private CharacterView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private Camera camera;
        [SerializeField] private SpriteRenderer background;
        
        private AnimationConfig _playerConfig;
        private AnimationConfig _coinConfig;
        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private ParallaxController _parallaxController;
        
        
        private void Awake()
        {
            _playerConfig = Resources.Load<AnimationConfig>("SpritePlayerConfig");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerAnimator.StartAnimation(playerView.spriteRenderer, AnimState.IdleBlink, true, 7.0f);
            
            _coinConfig = Resources.Load<AnimationConfig>("SpriteCoinConfig");
            _coinAnimator = new SpriteAnimatorController(_coinConfig);
            _coinAnimator.StartAnimation(coinView.spriteRenderer, AnimState.Idle, true, 10.0f);
        }

        private void Start()
        {
            _parallaxController = new ParallaxController(camera.transform, background.transform);
        }

        private void Update()
        {
            _playerAnimator.Execute();
            _coinAnimator.Execute();
            _parallaxController.Execute();
        }
    }
}
