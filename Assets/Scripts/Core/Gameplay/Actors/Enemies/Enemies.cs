using Core.Components;
using Core.Gameplay.Identity;
using Core.Gameplay.Scores;
using Core.Gameplay.Spawners;
using System;
using System.Collections.Generic;


namespace Core.Gameplay.Actors.Enemies
{
    public class Enemies : ITickable
    {
        private readonly ISpawner<IEnemy> spawner;
        private readonly IList<IEnemy> list;
        private readonly Score score;


        public Enemies(ISpawner<IEnemy> spawner, Score score, IdentityPool identityPool) :
            this(spawner, new List<IEnemy>(), score, identityPool) { }


        public Enemies(ISpawner<IEnemy> spawner, IList<IEnemy> list, Score score, IdentityPool identityPool)
        {
            this.spawner = spawner;
            this.list = list;
            this.score = score;


            spawner.Spawned += enemy =>
            {
                identityPool.Add(enemy.Id(), enemy);
                list.Add(enemy);
                enemy.Destroyed += () =>
                {
                    score.Add(1);
                    list.Remove(enemy);
                    enemy.Dispose();
                };
            };
        }


        public bool IsEnemy(Guid guid)
        {
            foreach (var enemy in list)
            {
                if (enemy.Id() == guid)
                {
                    return true;
                }
            }

            return false;
        }


        public void Dispose()
        {
            foreach (var enemy in list)
            {
                enemy.Dispose();
            }
        }


        public void Tick(float deltaTime)
        {
            spawner.Tick(deltaTime);
            foreach (var enemy in list)
            {
                enemy.Tick(deltaTime);
            }
        }
    }
}