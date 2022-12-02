using Core.Gameplay.Actors.Enemies.UFO;
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
    [CreateAssetMenu(fileName = "Ufo Config", menuName = "Asteroids/Ufo Config", order = 0)]
    public class UfoConfig : ScriptableObject
    {
        [SerializeField] private UfoParams? ufoConfig;
        [SerializeField] private PhysicalDragData? physicalDrag;


        public UfoParams UfoParams => ufoConfig.EnsureNotNull();


        public PhysicalDragData PhysicalDrag => physicalDrag.EnsureNotNull();
    }
}