using System;
using System.Collections.Generic;


namespace Core.Uitls
{
    public class ObjectPool<T> : IFactory<T>
    {
        private readonly Stack<T> cached;
        private readonly Func<T> construct;
        private readonly Action<T> onReturn;
        private readonly Action<T> onReuse;


        public ObjectPool(Func<T> construct, Action<T> onReturn, Action<T> onReuse, int startSize = 32)
        {
            this.construct = construct;
            this.onReturn = onReturn;
            this.onReuse = onReuse;
            cached = new Stack<T>(startSize);
            for (var i = 0; i < startSize; i++)
            {
                Return(construct());
            }
        }


        public T New()
        {
            if (cached.TryPop(out var item))
            {
                onReuse(item);
                return item;
            }

            return construct();
        }


        public void Return(T item)
        {
            onReturn(item);
            cached.Push(item);
        }
    }
}