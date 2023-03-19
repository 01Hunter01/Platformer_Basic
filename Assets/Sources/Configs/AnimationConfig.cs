using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    public sealed class AnimationConfig : ScriptableObject
    { 
        public List<SpriteSequence> sequenses = new List<SpriteSequence>();
    }
}
