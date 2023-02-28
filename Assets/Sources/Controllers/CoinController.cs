using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{ 
    public sealed class CoinController : MonoBehaviour
    {
        private AnimationConfig _coinConfig;
        private SpriteAnimatorController _coinAnimator;

        private float _animationSpeed = 10.0f;

        public CoinController(CoinView coinView)
        {
            _coinConfig = Resources.Load<AnimationConfig>("SpriteCoinConfig");
            _coinAnimator = new SpriteAnimatorController(_coinConfig);
            _coinAnimator.StartAnimation(coinView.spriteRenderer, AnimState.Idle, true, _animationSpeed);
        }

        public void Execute()
        {
            _coinAnimator.Execute();
        }
        
    }
}
