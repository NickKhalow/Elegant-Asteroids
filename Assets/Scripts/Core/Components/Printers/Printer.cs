using System.Collections.Generic;
using System.Text;


namespace Core.Components.Printers
{
    public class Printer : IPrinter
    {
        private readonly IDictionary<string, string> dictionary;
        private readonly StringBuilder stringBuilder;


        public Printer() : this(new Dictionary<string, string>()) { }


        public Printer(IDictionary<string, string> data)
        {
            dictionary = data;
            stringBuilder = new StringBuilder();
        }


        public void Add(string key, string data)
        {
            dictionary.Add(key, data);
        }


        public void Clear()
        {
            dictionary.Clear();
        }


        public string Value()
        {
            stringBuilder.Clear();
            foreach (var keyValuePair in dictionary)
            {
                stringBuilder.AppendLine($"{keyValuePair.Key}: {keyValuePair.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}