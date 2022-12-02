using Core.Components.Printers;
using Core.Gameplay.Guns.Cooldowns;
using DefaultNamespace;


namespace Core.Gameplay.Guns.Ammos
{
    public class RefillableAmmo : IAmmo
    {
        private readonly ICooldown cooldown;
        private readonly int maxCount;
        private int current;


        public RefillableAmmo(RefillableAmmoData ammoData) : this(
            new Cooldown(ammoData.refillDelay),
            ammoData.maxCount,
            ammoData.current) { }


        public RefillableAmmo(ICooldown cooldown, int maxCount, int current)
        {
            this.cooldown = cooldown;
            this.maxCount = maxCount;
            this.current = current;
        }


        public void Tick(float deltaTime)
        {
            cooldown.Tick(deltaTime);
            if (cooldown.NextReady())
            {
                if (current < maxCount)
                {
                    current++;
                }
            }
        }


        public bool WithdrawNext()
        {
            if (current > 0)
            {
                current--;
                return true;
            }

            return false;
        }


        public void Print(IPrinter printer)
        {
            printer.Add("Ammo Remains", current.ToString());
            if (maxCount > current)
            {
                printer.Add("Refill Delay", cooldown.DelayRemainsMessage());
            }
        }


        public void Dispose()
        {
            cooldown.Dispose();
            //
        }
    }
}