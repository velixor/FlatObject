namespace FlatObject
{
    public interface IFlatObjectFactory
    {
        IFlatObject Flatten(object obj);
    }
}