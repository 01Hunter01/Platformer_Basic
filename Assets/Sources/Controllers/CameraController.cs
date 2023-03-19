using UnityEngine;

namespace Platformer
{
    public sealed class CameraController
    {
        private Transform _mainCamera;
        private Transform _player;
        private Vector3 _offset;

        public CameraController(Transform mainCamera, Transform player)
        {
            _mainCamera = mainCamera;
            _player = player;
            _mainCamera.transform.position = new Vector3(_player.position.x, _player.position.y, _mainCamera.position.z);
            _offset = _mainCamera.position - _player.position;
        }

        public void Execute()
        {
            _mainCamera.position = _player.transform.position + _offset;
        }

    }
}
