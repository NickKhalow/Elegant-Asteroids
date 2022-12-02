using Core.Components;
using Core.Gameplay.Guns.Ammos;
using UnityEngine;


namespace Core.Gameplay.Guns
{
    public interface IGunView : IView<GunData>
    {
        Vector2 BarrelPosition();


        float BarrelRotation();
    }
}