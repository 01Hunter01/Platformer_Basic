using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class WaterManager
    {
        private float _animationSpeed = 8f;

        private List<LevelObjectView> _waterViews;
        private AnimationConfig _waterConfig;
        private SpriteAnimatorController _waterAnimator;

        public WaterManager(List<LevelObjectView> waterViews)
        {
            _waterViews = waterViews;
            _waterConfig = Resources.Load<AnimationConfig>("SpriteWaterConfig");
            _waterAnimator = new SpriteAnimatorController(_waterConfig);

            foreach (var waterView in _waterViews)
            {
                _waterAnimator.StartAnimation(waterView.spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }
        }

        public void Execute()
        {
            _waterAnimator.Execute();
        }
    }
}
