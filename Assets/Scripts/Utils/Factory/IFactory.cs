namespace Bug.Project21.Tools
{
    public interface IFactory<out T>
    {
        T Create();
    }
}