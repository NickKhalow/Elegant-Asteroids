using Core.Components;
using Core.Components.Printers;
using System.Diagnostics.SymbolStore;


namespace Core.Gameplay.Guns.Ammos
{
    public interface IAmmo : ITickable
    {
        bool WithdrawNext();


        void Print(IPrinter printer);
    }
}