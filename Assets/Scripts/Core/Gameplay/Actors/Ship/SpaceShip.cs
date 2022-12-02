using Core.Components;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Guns;
using Core.Gameplay.Identity;
using Core.Inputs;
using Core.Physics;
using Core.Uitls;
using System;
using UnityEngine;


namespace Core.Gameplay.Actors.Ship
{
    public class SpaceShip : ITickable, IDestroyable, ITarget
    {
        private readonly Physical physical;
        private readonly Gun laserGun;
        private readonly Gun bulletGun;
        private readonly IInput input;
        private readonly SpaceShipParams shipParams;
        private readonly IView<SpaceShipData> view;


        public SpaceShip(
            Physical physical,
            Gun laserGun,
            Gun bulletGun,
            IInput input,
            IView<SpaceShipData> view,
            SpaceShipParams shipParams,
            IdentityPool identityPool)
        {
            this.physical = physical;
            this.laserGun = laserGun;
            this.bulletGun = bulletGun;
            this.input = input;
            this.shipParams = shipParams;
            this.view = view;
            
            identityPool.Add(physical.Id(), this);

            physical.CollidedOther += data =>
            {
                Debug.Log($"Collide with {data}");
                if (identityPool.TryRead(data, out var o) && o is IEnemy)
                {
                    Destroyed?.Invoke();
                }
            };
        }
        

        public event Action? Destroyed;


        public void InstantKill()
        {
            Destroyed?.Invoke();
            Dispose();
        }


        public SpaceShipData Data()
        {
            return new SpaceShipData(
                shipParams,
                physical.Data(),
                laserGun.Data()
            );
        }


        public void Tick(float deltaTime)
        {
            physical.Add(input.Rotation()
                         * shipParams.rotatePower
                         * deltaTime);
            physical.Add(Vector2.up.Rotate(physical.Data().angle)
                         * (
                             input.Acceleration()
                             * shipParams.movePower
                             * deltaTime
                         )
            );
            TryShoot();

            physical.Tick(deltaTime);
            bulletGun.Tick(deltaTime);
            laserGun.Tick(deltaTime);
            view.Render(Data());
        }


        public Vector2 Position()
        {
            return physical.Position();
        }


        public void Dispose()
        {
            physical.Dispose();
            input.Dispose();
        }


        private void TryShoot()
        {
            if (input.ShootBullet())
            {
                bulletGun.TryShoot();
            }

            if (input.ShootLaser())
            {
                laserGun.TryShoot();
            }
        }
    }
}