using UnityEngine;

namespace Platformer
{
    public sealed class ParallaxController
    {
        private Transform _camera;
        private Transform _background;
        private Vector3 _cameraStartPosition;
        private Vector3 _backStartPosition;

        private const float Coef = 0.1f;
        
        public ParallaxController(Transform camera, Transform background)
        {
            _camera = camera;
            _background = background;
            _cameraStartPosition = camera.transform.position;
            _backStartPosition = background.transform.position;
        }

        public void Execute()
        {
            _background.position = _backStartPosition + (_camera.position - _cameraStartPosition);
        }
    }
}