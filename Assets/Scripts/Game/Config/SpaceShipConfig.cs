using Core.Gameplay.Actors.Ship;
using Core.Gameplay.Guns;
using Core.Gameplay.Guns.Ammos;
using Core.Gameplay.Guns.Cooldowns;
using Core.Gameplay.Guns.Projectile;
using Core.Physics;
using Core.Uitls;
using Game.Inputs;
using System;
using UnityEngine;
using View.Actors;
using View.Util;


namespace Game.Config
{
    [CreateAssetMenu(fileName = "SpaceShip Config", menuName = "Asteroids/SpaceShip Config", order = 0)]
    public class SpaceShipConfig : ScriptableObject
    {
        [SerializeField] private SpaceShipParams? spaceShipParams;
        [SerializeField] private PhysicalDragData? physicalDrag;
        [SerializeField] private GunConfig? laser;
        [SerializeField] private GunConfig? gun;
        [SerializeField] private RefillableAmmoData laserAmmo;


        public RefillableAmmoData LaserAmmo => laserAmmo;


        public GunConfig Laser => laser.EnsureNotNull();


        public GunConfig Gun => gun.EnsureNotNull();


        public SpaceShipParams SpaceShipParams => spaceShipParams.EnsureNotNull();


        public PhysicalDragData PhysicalDrag => physicalDrag.EnsureNotNull();


        [Serializable]
        public class GunConfig
        {
            public float cooldown = 1;
            public float projectileLifetime = 1;
            public float projectileSpeed = 0;
        }
    }
}