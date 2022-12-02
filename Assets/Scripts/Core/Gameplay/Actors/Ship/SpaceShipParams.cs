using System;


namespace Core.Gameplay.Actors.Ship
{
    [Serializable]
    public class SpaceShipParams
    {
        public float movePower;
        public float rotatePower;


        public SpaceShipParams(float movePower, float rotatePower)
        {
            this.movePower = movePower;
            this.rotatePower = rotatePower;
        }
    }
}