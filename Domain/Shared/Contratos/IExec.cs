using Domain.Shared.Entidade;

namespace Domain.Shared.Contratos
{
    public interface IExec<T> where T : ICommand
    {
         GenericResult Exec(T command);
    }
}
