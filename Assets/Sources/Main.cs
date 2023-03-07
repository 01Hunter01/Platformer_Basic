using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Plarformer
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private CannonView cannonView;
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer background;
        
        private ParallaxController _parallaxController;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            _coinController = new CoinController(coinView);
            _parallaxController = new ParallaxController(cam.transform, background.transform);
            _cannonController = new CannonController(cannonView.muzzleT, playerView.trans);
            _emitterController = new EmitterController(cannonView.bullets, cannonView.emitterT);
        }

        private void Update()
        {
            _coinController.Execute();
            _parallaxController.Execute();
            _cannonController.Execute();
            _emitterController.Execute();
        }

        private void FixedUpdate()
        {
            _playerController.Execute();
        }
    }
}
