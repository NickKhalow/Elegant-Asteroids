using System;


namespace Core.Gameplay.Guns.Ammos
{
    [Serializable]
    public struct RefillableAmmoData
    {
        public int maxCount;
        public float refillDelay;
        public int current;
    }
}