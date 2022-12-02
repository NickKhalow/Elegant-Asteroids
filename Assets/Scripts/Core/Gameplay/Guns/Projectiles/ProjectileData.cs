using UnityEngine;


namespace Core.Gameplay.Guns.Projectile
{
    public struct ProjectileData
    {
        public Vector2 position;
        public Vector2 direction;


        public ProjectileData(Vector2 position, Vector2 direction)
        {
            this.position = position;
            this.direction = direction;
        }
    }
}