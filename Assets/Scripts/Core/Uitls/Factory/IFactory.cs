namespace Core.Uitls
{
    public interface IFactory<out T>
    {
        T New();
    }


    public interface IFactory<out T, in TI>
    {
        T New(TI data);
    }
}