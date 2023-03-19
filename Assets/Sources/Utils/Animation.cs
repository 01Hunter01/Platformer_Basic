using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class Animation
    {
        public AnimState Track;
        public List<Sprite> Sprites;
        public bool Loop;
        public float Speed;
        public float Counter = 0;
        public bool Sleep;

        public void Execute()
        {
            if (Sleep) return;

            Counter += Time.deltaTime * Speed;

            if (Loop)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count;
                Sleep = true;
            }
        }
    }
}