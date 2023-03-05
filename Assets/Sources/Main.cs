using UnityEngine;
using UnityEngine.Serialization;

namespace Plarformer
{
    public sealed class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView playerView;
        [SerializeField] private CoinView coinView;
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer background;
        
        private ParallaxController _parallaxController;
        private PlayerController _playerController;
        private CoinController _coinController;
        
        private void Awake()
        {
            _playerController = new PlayerController(playerView);
            _coinController = new CoinController(coinView);
        }

        private void Start()
        {
            _parallaxController = new ParallaxController(cam.transform, background.transform);
        }

        private void Update()
        {
            _playerController.Execute();
            _coinController.Execute();
            _parallaxController.Execute();
        }
    }
}
