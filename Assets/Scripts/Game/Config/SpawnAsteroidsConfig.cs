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
using Random = UnityEngine.Random;


namespace Game.Config
{
    [CreateAssetMenu(fileName = "Asteroids Config", menuName = "Asteroids/Asteroids Config", order = 0)]
    public class SpawnAsteroidsConfig : ScriptableObject
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float maxAngularSpeed;
        [SerializeField] private PhysicalDragData? physicalDrag;
        [SerializeField] private int shardsCount;


        public int ShardsCount => shardsCount;


        public PhysicalDragData PhysicalDrag => physicalDrag.EnsureNotNull();


        public float RandomAngle => Random.Range(0, 360);


        public float RandomAngularSpeed => Random.value * maxAngularSpeed;


        public Vector2 RandomSpeed => Random.insideUnitCircle * maxSpeed;
    }
}