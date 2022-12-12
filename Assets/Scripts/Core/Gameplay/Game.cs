using Core.Components;
using Core.Gameplay.Actors.Enemies;
using Core.Gameplay.Actors.Ship;
using System;


namespace Core.Gameplay
{
    public class Game : ITickable
    {
        private readonly SpaceShip spaceShip;
        private readonly Enemies enemies;
        private bool gameOver = false;


        public Game(SpaceShip spaceShip, Enemies enemies)
        {
            this.spaceShip = spaceShip;
            this.enemies = enemies;

            spaceShip.Destroyed += () =>
            {
                gameOver = true;
                GameOver?.Invoke();
            };
        }


        public event Action? GameOver;


        public void Tick(float deltaTime)
        {
            if (gameOver)
            {
                return;
            }

            spaceShip.Tick(deltaTime);
            enemies.Tick(deltaTime);
        }


        public void Dispose()
        {
            spaceShip.Dispose();
            enemies.Dispose();
        }
    }
}