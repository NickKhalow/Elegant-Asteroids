using Core.Physics;
using System;
using UnityEngine;


namespace Core.Gameplay.Spawners.IImpulses
{
    public class CombinedImpulse : IImpulse
    {
        private readonly Func<Vector2> coordinate;
        private readonly Func<Vector2> speed;
        private readonly Func<float> angle;
        private readonly Func<float> angularSpeed;
        private readonly PhysicalDragData data;


        public CombinedImpulse(
            Func<Vector2> coordinate,
            Func<Vector2> speed,
            Func<float> angle,
            Func<float> angularSpeed,
            PhysicalDragData data
        )
        {
            this.coordinate = coordinate;
            this.speed = speed;
            this.angle = angle;
            this.angularSpeed = angularSpeed;
            this.data = data;
        }


        public PhysicalData Value()
        {
            return new PhysicalData(
                coordinate(),
                angle(),
                speed(),
                angularSpeed(),
                data
            );
        }


        public PhysicalData Value(Vector2 customCoordinates)
        {
            return new PhysicalData(
                customCoordinates,
                angle(),
                speed(),
                angularSpeed(),
                data
            );
        }
    }
}