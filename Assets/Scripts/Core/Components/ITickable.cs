using System;


namespace Core.Components
{
    public interface ITickable : IDisposable
    {
        void Tick(float deltaTime);
    }
}