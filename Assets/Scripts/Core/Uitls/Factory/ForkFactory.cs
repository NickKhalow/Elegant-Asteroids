using System;
using System.Collections.Generic;
using System.Linq;


namespace Core.Uitls
{
    public class ForkFactory<T> : IFactory<T>
    {
        private readonly IReadOnlyList<IFactory<T>> factories;
        private int currentIndex;


        public ForkFactory(params IFactory<T>[] factories) : this(factories.ToList()) { }


        public ForkFactory(IReadOnlyList<IFactory<T>> factories, int currentIndex = 0)
        {
            if (factories.Count == 0)
            {
                throw new ArgumentException("list is empty");
            }

            this.factories = factories;
            this.currentIndex = currentIndex;
        }


        public T New()
        {
            return factories[currentIndex++ % factories.Count].EnsureNotNull().New();
        }
    }
}