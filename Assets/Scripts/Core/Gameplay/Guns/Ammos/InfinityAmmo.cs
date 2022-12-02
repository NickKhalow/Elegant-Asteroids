using Core.Components.Printers;


namespace Core.Gameplay.Guns.Ammos
{
    public class InfinityAmmo : IAmmo
    {
        public bool WithdrawNext()
        {
            return true;
        }


        public void Print(IPrinter printer)
        {
            printer.Add("Ammo", "Infinity");
        }


        public void Dispose()
        {
            //ignore
        }


        public void Tick(float deltaTime)
        {
            //ignore
        }
    }
}