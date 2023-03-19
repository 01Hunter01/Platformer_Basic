using UnityEngine;

namespace Platformer
{
    public sealed class QuestObjectView : LevelObjectView
    {
        public Color _completedColor;
        public Color _defaultColor;
        public int id;

        private void Awake()
        {
            _defaultColor = spriteRenderer.color;
        }

        public void ProcessComplete()
        {
            spriteRenderer.color = _completedColor;
        }

        public void ProcessActivate()
        {
            spriteRenderer.color = _defaultColor;
        }
    }
}
