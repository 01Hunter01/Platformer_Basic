using UnityEngine;

namespace Plarformer
{
    public sealed class ParallaxController
    {
        private Transform _camera;
        private Transform _background;
        private Vector3 _cameraStartPosition;
        private Vector3 _backStartPosition;

        private const float Coef = 1.0f;
        
        public ParallaxController(Transform camera, Transform background)
        {
            _camera = camera;
            _background = background;
            _cameraStartPosition = camera.transform.position;
            _backStartPosition = background.transform.position;
        }

        public void Execute()
        {
            _background.position = _backStartPosition + (_camera.position - _cameraStartPosition) * Coef;
        }
    }
}