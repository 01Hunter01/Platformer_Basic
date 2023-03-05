using UnityEngine;
using UnityEngine.Serialization;

namespace Plarformer
{
    public sealed class LevelObjectView : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer; 
        public Transform trans;
         public Collider2D coll;
        public Rigidbody2D rigidBody;
    }
}
