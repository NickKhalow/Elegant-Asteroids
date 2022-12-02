namespace Core.Components.Printers
{
    public interface IPrinter
    {
        void Add(string key, string data);


        string Value();


        void Clear();
    }
}