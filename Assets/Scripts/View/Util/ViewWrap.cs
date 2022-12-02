using Core.Components;


namespace View.Util
{
    public class ViewWrap<TData> : IView<TData>
    {
        private readonly IView<TData>[] views;


        public ViewWrap(params IView<TData>[] views)
        {
            this.views = views;
        }


        public void Render(TData data)
        {
            foreach (var view in views)
            {
                view.Render(data);
            }
        }


        public void Dispose()
        {
            foreach (var view in views)
            {
                view.Dispose();
            }
        }
    }
}