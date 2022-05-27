namespace DotnetTEST.Data.Interfaces
{
    public interface IGetById<T, U> where T : class
    {
        T GetById(U id);
    }
}
