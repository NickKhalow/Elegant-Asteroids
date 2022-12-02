using Core.Components;


namespace DefaultNamespace
{
    public interface ICooldown : ITickable
    {
        bool NextReady();


        string DelayRemainsMessage();
    }
}