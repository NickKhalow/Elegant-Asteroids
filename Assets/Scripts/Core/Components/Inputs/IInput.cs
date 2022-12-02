using System;


namespace Core.Inputs
{
    public interface IInput : IDisposable
    {
        /// <returns>([-1:1])</returns>
        public float Rotation();


        /// <returns>([0:1])</returns>
        public float Acceleration();


        public bool ShootBullet();


        public bool ShootLaser();
    }
}