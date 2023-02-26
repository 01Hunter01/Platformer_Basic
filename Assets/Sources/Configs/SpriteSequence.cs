using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    [Serializable]
    public class SpriteSequence
    {
        public AnimState track;
        public List<Sprite> sprites = new List<Sprite>();
    }
}