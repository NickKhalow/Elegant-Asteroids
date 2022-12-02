using Core.Components;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Identity;
using Core.Physics;
using System;
using UnityEngine;


namespace Core.Gameplay.Guns.Projectiles
{
    public class Projectile : IProjectile
    {
        private readonly IdentityPool identityPool;
        private readonly IView<ProjectileData, CollideData> view;
        private ProjectileData projectileData;
        private readonly float speed;
        private readonly float lifeTime;
        private float currentTime;


        public Projectile(
            IdentityPool identityPool,
            IView<ProjectileData, CollideData> view,
            float lifeTime,
            float speed
        )
        {
            this.identityPool = identityPool;
            this.view = view;
            this.lifeTime = lifeTime;
            this.speed = speed;
        }


        public event Action Disposed;


        public void Start(in Vector2 position, in Vector2 direction)
        {
            projectileData = new ProjectileData(position, direction);
            view.Render(projectileData);
            view.Event += ViewOnEvent;
        }


        private void ViewOnEvent(CollideData obj)
        {
            if (identityPool.TryRead(obj.other, out var o) && o is IEnemy enemy)
            {
                enemy.InstantKill();
                Dispose();
                view.Event -= ViewOnEvent;
            }
        }


        public void Dispose()
        {
            Disposed?.Invoke();
            view.Dispose();
        }


        public void Tick(float deltaTime)
        {
            projectileData.position += projectileData.direction.normalized * (speed * deltaTime);
            view.Render(projectileData);

            currentTime += deltaTime;
            if (currentTime >= lifeTime)
            {
                Dispose();
            }
        }
    }
}