using DefaultNamespace;


namespace Core.Gameplay.Guns.Cooldowns
{
    public class Cooldown : ICooldown
    {
        private readonly float delay;
        private float countDown;


        public Cooldown(float delay)
        {
            this.delay = delay;
        }


        public void Tick(float deltaTime)
        {
            countDown -= deltaTime;
        }


        public bool NextReady()
        {
            if (countDown <= 0)
            {
                countDown = delay;
                return true;
            }

            return false;
        }


        public string DelayRemainsMessage()
        {
            return $"{countDown:0.00}";
        }


        public void Dispose()
        {
            //ignore
        }
    }
}