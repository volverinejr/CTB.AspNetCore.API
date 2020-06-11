using System.Collections.Generic;
using Domain.Shared.Entidade;

namespace Domain.Modulos.TaxaSelic
{
    public interface ITaxaSelicRepository
    {
        void Insert(TaxaSelicModel taxaSelicModel);
        void Update(TaxaSelicModel taxaSelicModel);
        void Delete(TaxaSelicModel taxaSelicModel);
        TaxaSelicModel GetById(int id);
        TaxaSelicResult GetById(int id, string usuario);
        IEnumerable<TaxaSelicResult> GetAll(Pesquisa pesquisa);
        int GetTotalDeRegistros();
        TaxaSelicModel GetByAnoeMes(int ano, int mes);
    }
}