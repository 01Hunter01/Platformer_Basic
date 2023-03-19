namespace Platformer
{
    public sealed class BulletView : LevelObjectView
    {
        private int _damagePoint = 10;

        public int DamagePoint
        {
            get => _damagePoint;
            set => _damagePoint = value;
        }
    }
}
