using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class EmitterController
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _emitterT;

        private int _index;
        private float _timeTillNextBullet;
        private float _startSpeed = 15f;
        private float _delay = 1f;

        public EmitterController(List<BulletView> bulletViews, Transform emitterT)
        {
            _emitterT = emitterT;
            foreach (BulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletView));
            }
        }

        public void Execute()
        {
            if (_timeTillNextBullet > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNextBullet -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBullet = _delay;
                _bulletControllers[_index].ThrowBullet(_emitterT.position, -_emitterT.up * _startSpeed);
                _index++;

                if (_index >= _bulletControllers.Count)
                {
                    _index = 0;
                }
            }
        }
    }
}
