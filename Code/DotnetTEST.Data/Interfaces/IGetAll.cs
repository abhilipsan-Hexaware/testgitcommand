using System.Collections.Generic;

namespace DotnetTEST.Data.Interfaces
{
    public interface IGetAll<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
