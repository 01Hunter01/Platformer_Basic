using System;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace Plarformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private Camera camera;
        [SerializeField] private SpriteRenderer background;
        
        private AnimationConfig _coinConfig;
        private SpriteAnimatorController _coinAnimator;
        private ParallaxController _parallaxController;
        private PlayerController _playerController;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            
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
            _playerController.Execute();
            _coinAnimator.Execute();
            _parallaxController.Execute();
        }
    }
}
