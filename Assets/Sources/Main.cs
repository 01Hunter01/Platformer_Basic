using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platformer
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView playerView;
        [SerializeField] private CannonView cannonView;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private List<LevelObjectView> waterViews;
        [SerializeField] private LevelObjectView enemyView;
        [SerializeField] private GeneratorLevelView generatorLevelView;
        [SerializeField] private List<QuestObjectView> coinViews;
        [SerializeField] private QuestView questView;

        private ParallaxController _parallaxController;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private CannonController _cannonController;
        private EmitterController _emitterController;
        private EnemyController _enemyController;
        private GeneratorLevelController _generatorLevelController;
        private QuestConfiguratorController _questConfiguratorController;
        
        private WaterManager _waterManager;
        private CoinManager _coinManager;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            _parallaxController = new ParallaxController(mainCamera.transform, background.transform);
            _cameraController = new CameraController(mainCamera.transform, playerView.transform);
            _cannonController = new CannonController(cannonView.muzzleT, playerView.trans);
            _emitterController = new EmitterController(cannonView.bullets, cannonView.emitterT);
            _enemyController = new EnemyController(enemyView, playerView);
            
            _generatorLevelController = new GeneratorLevelController(generatorLevelView);
            _generatorLevelController.Implement();
            
            _questConfiguratorController = new QuestConfiguratorController(questView, playerView);
            _questConfiguratorController.Initialization();

            _waterManager = new WaterManager(waterViews);
            _coinManager = new CoinManager(coinViews);
            
        }

        private void Update()
        {
            _cannonController.Execute();
            _emitterController.Execute();
            _waterManager.Execute();
            _coinManager.Execute();
        }

        private void FixedUpdate()
        {
            _playerController.Execute();
            _enemyController.Execute();
        }

        private void LateUpdate()
        {
            _parallaxController.Execute();
            _cameraController.Execute();
        }
    }
}
