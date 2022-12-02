using System;


namespace Core.Components
{
    public interface IView<in TData> : IDisposable
    {
        void Render(TData data);
    }


    public interface IView<in TData, out TEvent> : IView<TData>, IDisposable
    {
        event Action<TEvent> Event;
    }
}