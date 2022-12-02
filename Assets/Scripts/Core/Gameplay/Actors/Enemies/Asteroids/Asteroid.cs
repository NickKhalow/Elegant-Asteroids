using Core.Components;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Identity;
using Core.Physics;
using System;
using UnityEngine;


namespace Core.Gameplay.Actors.Asteroids
{
    public class Asteroid : IEnemy, ITarget
    {
        private readonly IdentityPool identityPool;
        private readonly Physical physical;


        public Asteroid(IdentityPool identityPool, Physical physical)
        {
            this.identityPool = identityPool;
            this.physical = physical;
            physical.CollidedOther += PhysicalOnCollidedOther;
        }


        public event Action? Destroyed;


        public void InstantKill()
        {
            Destroyed?.Invoke();
            Dispose();
        }


        public Guid Id()
        {
            return physical.Id();
        }


        private void PhysicalOnCollidedOther(Guid guid)
        {
            if (identityPool.TryRead(guid, out var o) && o is IProjectile)
            {
                InstantKill();
            }

            Debug.Log($"Collide with {guid}");
        }


        public void Dispose()
        {
            physical.Dispose();
        }


        public void Tick(float deltaTime)
        {
            physical.Tick(deltaTime);
        }


        public Vector2 Position()
        {
            return physical.Data().coordinates;
        }
    }
}