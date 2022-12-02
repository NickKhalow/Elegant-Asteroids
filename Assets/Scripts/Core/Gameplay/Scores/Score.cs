using Core.Components;


namespace Core.Gameplay.Scores
{
    public class Score
    {
        private readonly IView<int> view;
        private int current;


        public Score(IView<int> view, int current = 0)
        {
            this.view = view;
            this.current = current;
        }


        public void Add(int score)
        {
            current += score;
            view.Render(current);
        }
    }
}