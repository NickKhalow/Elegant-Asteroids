using Core.Uitls;
using DefaultNamespace;
using System;


namespace Core.Gameplay.Spawners
{
    public class TimedSpawner<T> : ISpawner<T>
    {
        private readonly IFactory<T> factory;
        private readonly ICooldown cooldown;


        public TimedSpawner(IFactory<T> factory, ICooldown cooldown)
        {
            this.factory = factory;
            this.cooldown = cooldown;
        }


        public event Action<T>? Spawned;


        public void Tick(float deltaTime)
        {
            cooldown.Tick(deltaTime);
            if (cooldown.NextReady())
            {
                Spawned?.Invoke(factory.New());
            }
        }

        public void Dispose()
        {
            cooldown.Dispose();
        }
    }
}