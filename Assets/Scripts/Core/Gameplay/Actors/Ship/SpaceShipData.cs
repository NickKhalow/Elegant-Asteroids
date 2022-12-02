using Core.Gameplay.Guns.Ammos;
using Core.Physics;


namespace Core.Gameplay.Actors.Ship
{
    public struct SpaceShipData
    {
        public SpaceShipParams shipParams;
        public PhysicalData physicalData;
        public GunData gunData;


        public SpaceShipData(SpaceShipParams shipParams, PhysicalData physicalData, GunData gunData)
        {
            this.shipParams = shipParams;
            this.physicalData = physicalData;
            this.gunData = gunData;
        }
    }
}