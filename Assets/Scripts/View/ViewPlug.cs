using Core.Components;


namespace View
{
    public class ViewPlug<TData> : IView<TData>
    {
        private TData? data;


        public void Render(TData data)
        {
            //ignore
            this.data = data;
        }


        public void Dispose()
        {
            //ignore
        }


        public TData? Data()
        {
            return data;
        }
    }
}