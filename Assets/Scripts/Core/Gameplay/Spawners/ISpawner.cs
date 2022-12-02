using Core.Components;
using System;


namespace Core.Gameplay.Spawners
{
    public interface ISpawner<out T> : ITickable
    {
        event Action<T>? Spawned;
    }
}