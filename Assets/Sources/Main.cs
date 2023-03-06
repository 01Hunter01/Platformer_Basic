using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Plarformer
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private CannonView cannonView;
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer background;
        
        private ParallaxController _parallaxController;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CannonController _cannonController;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            _coinController = new CoinController(coinView);
            _parallaxController = new ParallaxController(cam.transform, background.transform);
            _cannonController = new CannonController(cannonView.muzzleT, playerView.trans);
        }

        private void Update()
        {
            _coinController.Execute();
            _parallaxController.Execute();
            _cannonController.Execute();
        }

        private void FixedUpdate()
        {
            _playerController.Execute();
        }
    }
}
