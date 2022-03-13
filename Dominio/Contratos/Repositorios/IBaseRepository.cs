using Dominio.Entidades;
using System.Linq.Expressions;

namespace Dominio.Contratos.Repositorios;
public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entidade);
    Task RemoverAsync(Guid id);
    Task AtualizarAsync(TEntity entidade);
    Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> BuscarPorIdAsync(Guid id);
}
