using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plarformer
{
    public class SpriteAnimatorController : IDisposable
    {
        private AnimationConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorController(AnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if(_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.sequenses.Find(sequence => sequence.track == track).sprites;
                    animation.Counter = 0;
                    
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.sequenses.Find(sequence => sequence.track == track).sprites,
                    Loop = loop,
                    Speed = speed,
                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }
        
        public void Execute()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Execute();

                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
