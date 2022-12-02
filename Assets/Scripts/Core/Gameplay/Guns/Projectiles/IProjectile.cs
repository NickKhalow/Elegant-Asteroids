using Core.Components;
using System;
using UnityEngine;


namespace Core.Gameplay.Guns.Projectile
{
    public interface IProjectile : ITickable
    {
        public event Action Disposed;
        
        void Start(in Vector2 position, in Vector2 direction);
    }
}