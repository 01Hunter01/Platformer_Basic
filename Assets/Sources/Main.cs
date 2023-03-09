using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private CannonView cannonView;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private List<LevelObjectView> waterViews;

        private ParallaxController _parallaxController;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CannonController _cannonController;
        private EmitterController _emitterController;

        private WaterManager _waterManager;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            _coinController = new CoinController(coinView);
            _parallaxController = new ParallaxController(mainCamera.transform, background.transform);
            _cameraController = new CameraController(mainCamera.transform, playerView.transform);
            _cannonController = new CannonController(cannonView.muzzleT, playerView.trans);
            _emitterController = new EmitterController(cannonView.bullets, cannonView.emitterT);

            _waterManager = new WaterManager(waterViews);
        }

        private void Update()
        {
            _coinController.Execute();
            _cannonController.Execute();
            _emitterController.Execute();
            _waterManager.Execute();
        }

        private void FixedUpdate()
        {
            _playerController.Execute();
        }

        private void LateUpdate()
        {
            _parallaxController.Execute();
            _cameraController.Execute();
        }
    }
}
