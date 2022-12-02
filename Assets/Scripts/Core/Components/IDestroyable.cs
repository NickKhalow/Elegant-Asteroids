using System;


namespace Core.Components
{
    public interface IDestroyable
    {
        event Action Destroyed;


        void InstantKill();
    }
}