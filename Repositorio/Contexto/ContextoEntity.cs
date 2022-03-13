using MongoDB.Driver;

namespace Repositorio.Contexto;
public class ContextoMongo
{
    internal IMongoDatabase MongoDataBase { get; }
    internal const string AlteracoesCollectionName = "ALTERACOES";

    public ContextoMongo(string connectionString, string dataBase)
    {
        var clientMongo = new MongoClient(connectionString);
        MongoDataBase = clientMongo.GetDatabase(dataBase);
    }
}
