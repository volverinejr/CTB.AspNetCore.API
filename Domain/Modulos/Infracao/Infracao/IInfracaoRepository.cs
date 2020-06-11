using System;
using System.Collections.Generic;
using Domain.Shared.Entidade;

namespace Domain.Modulos.Infracao.Infracao
{
    public interface IInfracaoRepository
    {
        void Insert(InfracaoModel infracaoModel);
        void Update(InfracaoModel infracaoModel);
        void Delete(InfracaoModel infracaoModel);
        InfracaoModel GetById(int id);
        InfracaoModel GetById(int id, string usuario);
        IEnumerable<InfracaoModel> GetByCodigo(string codigo);
        InfracaoModel GetByCodigoEValidadeInicio(string codigo, DateTime validadeInicio);
        IEnumerable<InfracaoModel> GetAll(Pesquisa pesquisa);
        InfracaoModel GetByCodigoValidadeInicioMenor(int id, string codigo, DateTime validadeInicio);
        InfracaoModel GetByCodigoValidadeInicioMaior(int id, string codigo, DateTime validadeInicio);
        int GetTotalDeRegistros();
    }
}