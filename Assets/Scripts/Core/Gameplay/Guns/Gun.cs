using Core.Components.Printers;
using Core.Gameplay.Guns.Ammos;
using Core.Gameplay.Guns.Projectile;
using Core.Uitls;
using DefaultNamespace;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Gameplay.Guns
{
    public class Gun : IGun
    {
        private readonly ICooldown cooldown;
        private readonly IAmmo ammo;
        private readonly IFactory<IProjectile> projectileFactory;
        private readonly IGunView gunView;
        private readonly IPrinter printer;
        private readonly IList<IProjectile> projectiles;


        public Gun(
            ICooldown cooldown,
            IAmmo ammo,
            IFactory<IProjectile> projectileFactory,
            IGunView gunView,
            IPrinter printer
        )
        {
            this.cooldown = cooldown;
            this.ammo = ammo;
            this.projectileFactory = projectileFactory;
            this.gunView = gunView;
            this.printer = printer;
            projectiles = new List<IProjectile>();
        }




        public void Dispose()
        {
            cooldown.Dispose();
        }


        public void Tick(float deltaTime)
        {
            ammo.Tick(deltaTime);
            cooldown.Tick(deltaTime);
            for (var i = projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = projectiles[i];
                projectile.Tick(deltaTime);
            }
        }


        public void TryShoot()
        {
            if (cooldown.NextReady() && ammo.WithdrawNext())
            {
                var projectile = projectileFactory.New();
                
                projectiles.Add(projectile);
                projectile.Disposed += () =>
                {
                    projectiles.Remove(projectile);
                };
                
                projectile.Start(
                    gunView.BarrelPosition(),
                    Vector2.up.Rotate(
                        gunView.BarrelRotation()
                    )
                );
            }
        }


        public GunData Data()
        {
            printer.Clear();
            ammo.Print(printer);
            return new GunData(printer.Value());
        }
    }
}