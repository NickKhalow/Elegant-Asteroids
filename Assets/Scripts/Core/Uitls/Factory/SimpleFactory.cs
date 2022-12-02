using Unity.Plastic.Newtonsoft.Json.Serialization;


namespace Core.Uitls
{
    public class SimpleFactory<T> : IFactory<T>
    {
        private readonly Func<T> constructor;


        public SimpleFactory(Func<T> constructor)
        {
            this.constructor = constructor;
        }


        public T New()
        {
            return constructor();
        }
    }
}