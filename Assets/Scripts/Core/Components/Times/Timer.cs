using Core.Components;


namespace Core.Times
{
    public class Timer : ITickable
    {
        private readonly float period;
        private float passedTime;


        public Timer(float period)
        {
            this.period = period;
        }


        public bool Done()
        {
            return passedTime >= period;
        }


        public void Tick(float deltaTime)
        {
            passedTime += deltaTime;
        }


        public void Dispose()
        {
            //ignore
        }
    }
}