using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public sealed class CannonView : MonoBehaviour
    {
        public Transform muzzleT;
        public Transform emitterT;
        public List<BulletView> bullets;
    }
}
