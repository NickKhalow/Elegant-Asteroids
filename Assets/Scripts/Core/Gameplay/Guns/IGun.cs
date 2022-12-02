using Core.Components;
using Core.Gameplay.Guns.Ammos;
using UnityEngine;


namespace Core.Gameplay.Guns
{
    public interface IGun : ITickable
    {
        void TryShoot();


        GunData Data();
    }
}