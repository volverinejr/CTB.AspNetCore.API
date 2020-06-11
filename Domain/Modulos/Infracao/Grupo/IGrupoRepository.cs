using System.Collections.Generic;
using Domain.Shared.Entidade;

namespace Domain.Modulos.Infracao.Grupo
{
    public interface IGrupoRepository
    {
        void Insert(GrupoModel grupoModel);
        void Update(GrupoModel grupoModel);
        void Delete(GrupoModel grupoModel);
        GrupoModel GetById(int id);
        GrupoResult GetById(int id, string usuario);
        IEnumerable<GrupoResult> GetAll(Pesquisa pesquisa);
        int GetTotalDeRegistros();
    }
}