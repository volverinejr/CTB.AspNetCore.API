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
        TaxaSelicModel GetById(int id, string usuario);
        IEnumerable<TaxaSelicModel> GetAll(Pesquisa pesquisa);
        int GetTotalDeRegistros(Pesquisa pesquisa);
        TaxaSelicModel GetByAnoeMes(int ano, int mes);
    }
}