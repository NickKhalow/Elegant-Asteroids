using System;
using UnityEngine;


namespace Core.Physics
{
    [Serializable]
    public class PhysicalData
    {
        public Vector2 coordinates;
        public float angle;
        public Vector2 speed;
        public float angularSpeed;
        public PhysicalDragData drag;


        public PhysicalData(Vector2 coordinates, float angle, Vector2 speed, float angularSpeed, PhysicalDragData drag)
        {
            this.coordinates = coordinates;
            this.angle = angle;
            this.speed = speed;
            this.angularSpeed = angularSpeed;
            this.drag = drag;
        }
    }
}