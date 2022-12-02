using Core.Components;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Actors.Enemies.UFO;
using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Identity;
using Core.Physics;
using System;
using UnityEngine;


namespace Core.Gameplay.Actors.UFO
{
    public class Ufo : IEnemy
    {
        private readonly IdentityPool identityPool;
        private readonly UfoParams @params;
        private readonly Physical physical;
        private readonly ITarget target;


        public Ufo(IdentityPool identityPool, UfoParams @params, Physical physical, ITarget target)
        {
            this.identityPool = identityPool;
            this.@params = @params;
            this.physical = physical;
            this.target = target;
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
                Debug.Log("Ufo destroyed");
                Destroyed?.Invoke();
            }   
            Debug.Log($"Collide with {guid}");
        }


        public void Dispose()
        {
            physical.Dispose();
        }


        public void Tick(float deltaTime)
        {
            var me = physical.Data().coordinates;
            var other = target.Position();
            var distance = other - me;

            physical.Add(distance.normalized * (@params.movePower * deltaTime));
            physical.Tick(deltaTime);
        }
    }
}