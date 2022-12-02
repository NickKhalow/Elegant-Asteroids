using Core.Inputs;
using Core.Uitls;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Game.Inputs
{
    public class PlayerInput : IInput
    {
        private readonly DefaultInput defaultInput;
        private readonly InputAction move;
        private readonly InputAction fireBullet;
        private readonly InputAction fireLaser;


        public PlayerInput(DefaultInput defaultInput)
        {
            this.defaultInput = defaultInput;
            move = defaultInput.Player.Move.EnsureNotNull();
            fireBullet = defaultInput.Player.FireBullet.EnsureNotNull();
            fireLaser = defaultInput.Player.FireLaser.EnsureNotNull();
            defaultInput.Enable();
        }


        public PlayerInput() : this(new DefaultInput()) { }


        public float Rotation()
        {
            return -move.ReadValue<Vector2>().x;
        }


        public float Acceleration()
        {
            return Mathf.Clamp(move.ReadValue<Vector2>().y, 0, 1);
        }


        public bool ShootBullet()
        {
            return fireBullet.IsPressed();
        }


        public bool ShootLaser()
        {
            return fireLaser.IsPressed();
        }


        public void Dispose()
        {
            defaultInput.Dispose();
            move.Dispose();
            fireBullet.Dispose();
            fireLaser.Dispose();
        }
    }
}