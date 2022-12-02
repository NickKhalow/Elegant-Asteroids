using Core.Physics;
using System;


namespace Core.Gameplay.Actors.Enemies.UFO
{
    [Serializable]
    public class UfoParams
    {
        public float movePower;


        public UfoParams(float movePower)
        {
            this.movePower = movePower;
        }
    }
}