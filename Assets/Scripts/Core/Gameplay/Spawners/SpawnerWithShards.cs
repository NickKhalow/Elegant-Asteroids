using Core.Components.Physics;
using Core.Gameplay.Actors.Asteroids;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Identity;
using Core.Gameplay.Spawners.IImpulses;
using Core.Physics;
using Core.Uitls;
using System;
using UnityEngine;


namespace Core.Gameplay.Spawners
{
    public class SpawnerWithShards : ISpawner<IEnemy>
    {
        private readonly IdentityPool identityPool;
        private readonly int shardsCountPerDestroy;
        private readonly IFactory<IPhysicalView> physicalViewFactory;
        private readonly CombinedImpulse combinedImpulse;
        private readonly Vector2 boundaries;
        private readonly ISpawner<IEnemy> spawner;


        public event Action<IEnemy>? Spawned;

        
        public SpawnerWithShards(
            IdentityPool identityPool,
            ISpawner<IEnemy> spawner,
            IFactory<IPhysicalView> physicalViewFactory,
            CombinedImpulse combinedImpulse,
            int shardsCountPerDestroy,
            Vector2 boundaries
        )
        {
            this.identityPool = identityPool;
            this.shardsCountPerDestroy = shardsCountPerDestroy;
            this.physicalViewFactory = physicalViewFactory;
            this.combinedImpulse = combinedImpulse;
            this.boundaries = boundaries;
            this.spawner = spawner;
            spawner.Spawned += SpawnerOnSpawned;
        }


        private void SpawnerOnSpawned(IEnemy obj)
        {
            if (obj is Asteroid asteroid)
            {
                void OnAsteroidDestroyed()
                {
                    var position = asteroid.Position();
                    for (int i = 0; i < shardsCountPerDestroy; i++)
                    {
                        Spawned?.Invoke(new Asteroid(
                                identityPool,
                                new Physical(
                                    combinedImpulse.Value(position),
                                    boundaries,
                                    physicalViewFactory.New()
                                )
                            )
                        );
                    }

                    asteroid.Destroyed -= OnAsteroidDestroyed;
                }

                asteroid.Destroyed += OnAsteroidDestroyed;
            }

            Spawned?.Invoke(obj);
        }


        public void Dispose()
        {
            spawner.Dispose();
        }


        public void Tick(float deltaTime)
        {
            spawner.Tick(deltaTime);
        }
    }
}