using System;
using UnityEngine;

namespace Plarformer
{
    public sealed class InteractiveObjectView : LevelObjectView
    {
        public Action<BulletView> TakeDamage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out BulletView contactView))
            {
                TakeDamage?.Invoke(contactView);
            }
        }
    }
}