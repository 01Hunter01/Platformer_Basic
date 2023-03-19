using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class CoinManager
    {
        
        private float _animationSpeed = 8f;

        private List<QuestObjectView> _coinViews;
        private AnimationConfig _coinConfig;
        private SpriteAnimatorController _coinAnimator;

        public CoinManager(List<QuestObjectView> coinViews)
        {
            _coinViews = coinViews;
            _coinConfig = Resources.Load<AnimationConfig>("SpriteCoinConfig");
            _coinAnimator = new SpriteAnimatorController(_coinConfig);

            foreach (var coinView in _coinViews)
            {
                _coinAnimator.StartAnimation(coinView.spriteRenderer, AnimState.Idle, true, _animationSpeed);
            }
        }

        public void Execute()
        {
            _coinAnimator.Execute();
        }
        
    }
}
