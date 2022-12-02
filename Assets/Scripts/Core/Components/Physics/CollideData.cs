using System;


namespace Core.Physics
{
    public readonly struct CollideData
    {
        public readonly Guid other;


        public CollideData(Guid other)
        {
            this.other = other;
        }
    }
}