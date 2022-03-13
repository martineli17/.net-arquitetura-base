using Dominio.Contratos.Repositorios;
using Dominio.Entidades;
using MongoDB.Driver;
using Repositorio.Contexto;
using Repositorio.Eventos;
using System.Linq.Expressions;

namespace Repositorio.Repositorios.Base
{
    public abstract class BaseRepository<TEntidade> : IBaseRepository<TEntidade> where TEntidade : Entity
    {
        private readonly IMongoCollection<BaseEvento<TEntidade>> _collectionAlteracoes;
        protected readonly IMongoCollection<TEntidade> Collection;
        protected string CollectionName;
        protected BaseRepositoryInjector Injector;

        public BaseRepository(BaseRepositoryInjector injector)
        {
            Injector = injector;
            Collection = Injector.Context.MongoDataBase.GetCollection<TEntidade>(GetNameCollection());
            _collectionAlteracoes = Injector.Context.MongoDataBase.GetCollection<BaseEvento<TEntidade>>(ContextoMongo.AlteracoesCollectionName);
        }
        public async Task AddAsync(TEntidade entidade)
        {
            await Collection.InsertOneAsync(entidade);
            await AddAsync(entidade, EnumTipoEvento.Adicao);
        }

        public async Task<IEnumerable<TEntidade>> BuscarAsync(Expression<Func<TEntidade, bool>> filter)
        {
            if (filter == null) return (await Collection.FindAsync(x => x.Id != Guid.Empty)).ToEnumerable();
            return (await Collection.FindAsync(Builders<TEntidade>.Filter.Where(filter))).ToEnumerable();
        }

        public async Task<TEntidade> BuscarPorIdAsync(Guid id)
        {
            return (await Collection.FindAsync(Builders<TEntidade>.Filter.Where(x => x.Id.Equals(id))))?.FirstOrDefault();
        }

        public async Task RemoverAsync(Guid idEntidade)
        {
            var entidade = await BuscarPorIdAsync(idEntidade);
            if (entidade is null) return;
            await AddAsync(entidade, EnumTipoEvento.Remocao);
            await Collection.DeleteOneAsync(x => x.Id.Equals(idEntidade));
        }

        public async Task AtualizarAsync(TEntidade entidade)
        {
            await Collection.ReplaceOneAsync(x => x.Id.Equals(entidade.Id), entidade);
            await AddAsync(entidade, EnumTipoEvento.Atualizacao);
        }

        protected IAggregateFluent<TEntidade> GetAggregate()
        {
            return Collection.Aggregate();
        }

        protected IMongoCollection<TType> GetCollection<TType>(string name)
        {
            return Injector.Context.MongoDataBase.GetCollection<TType>(name);
        }

        protected async Task<IEnumerable<TEntidade>> Buscarsync(FilterDefinition<TEntidade> filter)
        {
            if (filter == null) return (await Collection.FindAsync(x => x.Id != Guid.Empty)).ToEnumerable();
            return (await Collection.FindAsync(filter)).ToEnumerable();
        }

        protected abstract string GetNameCollection();

        private async Task AddAsync(TEntidade entidade, EnumTipoEvento tipoEvento)
        {
            await _collectionAlteracoes.InsertOneAsync(BaseEvento<TEntidade>.Creator(entidade.Id, entidade, tipoEvento));
        }
    }
}
