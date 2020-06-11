using System.Collections.Generic;
using Domain.Shared.Entidade;

namespace Domain.Modulos.Infracao.Natureza
{
    public interface INaturezaRepository
    {
        void Insert(NaturezaModel naturezaModel);
        void Update(NaturezaModel naturezaModel);
        void Delete(NaturezaModel naturezaModel);
        NaturezaModel GetById(int id);
        NaturezaResult GetById(int id, string usuario);
        IEnumerable<NaturezaResult> GetAll(Pesquisa pesquisa);
        int GetTotalDeRegistros();
    }
}