using MongoDB.Driver;

namespace DotnetTEST.Data.Interfaces
{
    public interface IGateway
    {
        IMongoDatabase GetMongoDB();
    }
}
