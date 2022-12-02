using System;
using System.Collections.Generic;


namespace Core.Gameplay.Identity
{
    public class IdentityPool
    {
        private readonly IDictionary<Guid, object> dictionary;


        public IdentityPool() : this(new Dictionary<Guid, object>()) { }


        public IdentityPool(IDictionary<Guid, object> dictionary)
        {
            this.dictionary = dictionary;
        }


        public void Add(Guid guid, object unique)
        {
            dictionary.Add(guid, unique);
        }


        public object? Read(Guid guid)
        {
            if (dictionary.TryGetValue(guid, out var value))
            {
                return value;
            }

            throw new InvalidOperationException($"Object {guid} not found");
        }


        public bool TryRead(Guid guid, out object? value)
        {
            return dictionary.TryGetValue(guid, out value);
        }
    }
}