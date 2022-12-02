using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Guns.Projectiles;
using Core.Physics;
using Core.Uitls;
using System;
using UnityEngine;
using View.Actors;


namespace View.Projectiles
{
    [RequireComponent(typeof(GenericPhysicalView))]
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        private GenericPhysicalView genericPhysicalView = null!;


        public event Action? Disposed;


        public event Action<CollideData>? Event;


        private void Awake()
        {
            genericPhysicalView = GetComponent<GenericPhysicalView>().EnsureNotNull();
            genericPhysicalView.Event += data =>
            {
                Debug.Log($"Data: {data.other}");
                Event?.Invoke(data);
            };
        }


        public void Render(ProjectileData data)
        {
            transform.position = data.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, data.direction);
        }


        public void Dispose()
        {
            Disposed?.Invoke();
        }
    }
}