using Core.Components;
using Core.Components.Physics;
using System;
using UnityEngine;


namespace Core.Physics
{
    public delegate void CollidedWith(Guid other);


    public class Physical : ITickable, ITarget
    {
        private readonly Guid guid;
        private readonly IPhysicalView view;
        private PhysicalData data;
        private readonly Vector2 boundaries;


        public event CollidedWith? CollidedOther;


        public Physical(
            Guid guid,
            PhysicalData physicalData,
            Vector2 boundaries,
            IPhysicalView view
        )
        {
            this.guid = guid;
            data = physicalData;
            this.view = view;
            this.boundaries = boundaries;

            view.Event += collideData => CollidedOther?.Invoke(collideData.other);
        }


        public Physical(
            PhysicalData physicalData,
            Vector2 boundaries,
            IPhysicalView view
        ) : this(Guid.NewGuid(), physicalData, boundaries, view) { }


        public Guid Id()
        {
            return guid;
        }


        public PhysicalData Data()
        {
            return data;
        }


        public Vector2 Position()
        {
            return data.coordinates;
        }


        public void Add(Vector2 acceleration)
        {
            data.speed += acceleration;
        }


        public void Add(float torque)
        {
            data.angularSpeed += torque;
        }


        public void Tick(float deltaTime)
        {
            ApplySpeed(deltaTime);
            ApplyTorque(deltaTime);
            ApplyDrag(deltaTime);
            ApplyAngularDrag(deltaTime);
            ApplyBoundary();

            view.Render((guid, data));
        }


        private void ApplySpeed(float deltaTime)
        {
            data.coordinates += data.speed * deltaTime;
        }


        private void ApplyTorque(float deltaTime)
        {
            data.angle += data.angularSpeed * deltaTime;
        }


        private void ApplyDrag(float deltaTime)
        {
            data.speed = Vector2.MoveTowards(
                data.speed,
                Vector2.zero,
                data.drag.linear * Mathf.Pow(data.speed.magnitude, 2) * deltaTime);
        }


        private void ApplyAngularDrag(float deltaTime)
        {
            data.angularSpeed = Mathf.MoveTowards(
                data.angularSpeed,
                0,
                data.drag.angular * Mathf.Pow(data.angularSpeed, 2) * deltaTime);
        }


        private void ApplyBoundary()
        {
            static float Clamp(float value, float boundary)
            {
                if (value > boundary)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = boundary;
                }

                return value;
            }

            data.coordinates.x = Clamp(
                data.coordinates.x,
                boundaries.x
            );
            data.coordinates.y = Clamp(
                data.coordinates.y,
                boundaries.y
            );
        }


        public void Dispose()
        {
            view.Dispose();
        }
    }
}