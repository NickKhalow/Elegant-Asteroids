using Core.Components;
using System;


namespace Core.Gameplay.Actors.Enemies
{
    public interface IEnemy : ITickable, IDestroyable
    {
        public Guid Id();
    }
}