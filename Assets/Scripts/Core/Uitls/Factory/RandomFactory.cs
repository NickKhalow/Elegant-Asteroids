using System;
using System.Collections.Generic;
using System.Linq;


namespace Core.Uitls
{
    public class RandomFactory<T> : IFactory<T>
    {
        private readonly IReadOnlyList<IFactory<T>> factories;


        public RandomFactory(params IFactory<T>[] factories) : this(factories.ToList()) { }


        public RandomFactory(IReadOnlyList<IFactory<T>> factories)
        {
            if (factories.Count == 0)
            {
                throw new ArgumentException("list is empty");
            }

            this.factories = factories;
        }


        public T New()
        {
            return factories[UnityEngine.Random.Range(0, factories.Count)].EnsureNotNull().New();
        }
    }
}