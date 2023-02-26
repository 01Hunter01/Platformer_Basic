using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Plarformer
{
    [CreateAssetMenu(fileName = "SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    public class AnimationConfig : ScriptableObject
    { 
        public List<SpriteSequence> sequenses = new List<SpriteSequence>();
    }
}
