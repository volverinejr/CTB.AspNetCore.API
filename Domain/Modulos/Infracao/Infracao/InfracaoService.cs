using System;
using System.Collections.Generic;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Infracao.Command;
using Domain.Modulos.Infracao.Natureza;
using Domain.Shared.Contratos;
using Domain.Shared.Entidade;
using Flunt.Notifications;


namespace Domain.Modulos.Infracao.Infracao
{
    public class InfracaoService : Notifiable,
        IExec<InfracaoInsertCommand>,
        IExec<InfracaoUpdateCommand>,
        IExec<InfracaoDeleteCommand>
    {
        private readonly IInfracaoRepository _repository;
        private readonly IGrupoRepository _repositoryGrupo;
        private readonly INaturezaRepository _repositoryNatureza;

        public InfracaoService(IInfracaoRepository repository, IGrupoRepository repositoryGrupo, INaturezaRepository repositoryNatureza)
        {
            _repository = repository;
            _repositoryGrupo = repositoryGrupo;
            _repositoryNatureza = repositoryNatureza;
        }

        public GenericResult Exec(InfracaoInsertCommand command)
        {
            GrupoModel grupoModel = _repositoryGrupo.GetById(command.GrupoId);
            if (grupoModel == null)
            {
                command.AddNotification("GrupoId", "Grupo Inexistente");
            }

            NaturezaModel naturezaModel = _repositoryNatureza.GetById(command.NaturezaId);
            if (naturezaModel == null)
            {
                command.AddNotification("Natureza", "Natureza Inexistente");
            }

            InfracaoModel model = _repository.GetByCodigoEValidadeInicio(command.Codigo, command.ValidadeInicio);
            if (model != null)
            {
                command.AddNotification("Codigo e ValidadeInicio", "Par (Código e Validade) já existente para o id:" + model.Id.ToString());
            }



            command.Validate();
            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido", command.Notifications);
            }


            model = new InfracaoModel(
                command.Codigo, command.Descricao, command.AmparoLegal, command.MedidaAdm, command.Crime, command.ObsObrigatoria, command.GrupoId, command.NaturezaId,
                command.CompetenciaMunicipal, command.CompetenciaEstadual,
                command.CompetenciaRodoviaria, command.InfratorCondutor, command.InfratorProprietario,
                command.ValidadeInicio
            );



            // inicio Enquadrando
            InfracaoModel infracaoMenor = _repository.GetByCodigoValidadeInicioMenor(0, command.Codigo, command.ValidadeInicio);
            InfracaoModel infracaoMaior = _repository.GetByCodigoValidadeInicioMaior(0, command.Codigo, command.ValidadeInicio);


            if (infracaoMenor != null)
            {
                infracaoMenor.SetDataFim(command.ValidadeInicio.AddDays(-1));
            }

            if (infracaoMaior != null)
            {
                model.SetDataFim(infracaoMaior.ValidadeInicio.AddDays(-1));
            }
            // fim Enquadrando


            try
            {
                if (infracaoMenor != null)
                {
                    _repository.Update(infracaoMenor);
                }

                _repository.Insert(model);

                model = _repository.GetById(model.Id);

                return new GenericResult(201, "Recurso Inserido", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }


        public GenericResult Exec(InfracaoUpdateCommand command)
        {
            GrupoModel grupoModel = _repositoryGrupo.GetById(command.GrupoId);
            if (grupoModel == null)
            {
                command.AddNotification("GrupoId", "Grupo Inexistente");
            }

            NaturezaModel naturezaModel = _repositoryNatureza.GetById(command.NaturezaId);
            if (naturezaModel == null)
            {
                command.AddNotification("Natureza", "Natureza Inexistente");
            }

            InfracaoModel model = _repository.GetByCodigoEValidadeInicio(command.Codigo, command.ValidadeInicio);
            if ((model != null) && (model.Id != command.Id))
            {
                command.AddNotification("Codigo e ValidadeInicio", "Par (Código e Validade) já existente para o id:" + model.Id.ToString());
            }


            model = _repository.GetById(command.Id);
            if (model == null)
            {
                command.AddNotification("Id", "Recurso Inexistente");
            }


            command.Validate();
            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido", command.Notifications);
            }



            model.Atualizar(
                command.Codigo, command.Descricao, command.AmparoLegal, command.MedidaAdm, command.Crime, command.ObsObrigatoria, command.GrupoId, command.NaturezaId,
                command.CompetenciaMunicipal, command.CompetenciaEstadual, command.CompetenciaRodoviaria,
                command.InfratorCondutor, command.InfratorProprietario,
                command.ValidadeInicio
            );


            // inicio Enquadrando
            InfracaoModel infracaoMenor = _repository.GetByCodigoValidadeInicioMenor(command.Id, command.Codigo, command.ValidadeInicio);
            InfracaoModel infracaoMaior = _repository.GetByCodigoValidadeInicioMaior(command.Id, command.Codigo, command.ValidadeInicio);


            if (infracaoMenor != null)
            {
                infracaoMenor.SetDataFim(command.ValidadeInicio.AddDays(-1));
            }

            if (infracaoMaior != null)
            {
                model.SetDataFim(infracaoMaior.ValidadeInicio.AddDays(-1));
            }
            // fim Enquadrando




            try
            {
                if (infracaoMenor != null)
                {
                    _repository.Update(infracaoMenor);
                }

                _repository.Update(model);

                model = _repository.GetById(model.Id);

                return new GenericResult(200, "Recurso Atualizado", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }

        public GenericResult Exec(InfracaoDeleteCommand command)
        {
            InfracaoModel model = _repository.GetById(command.Id);
            if (model == null)
            {
                command.AddNotification("Id", "Recurso Inexistente");
            }


            command.Validate();
            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido", command.Notifications);
            }


            try
            {
                _repository.Delete(model);

                return new GenericResult(200, "Recurso Removido", null);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }


        public IEnumerable<InfracaoModel> GetAll(Pesquisa pesquisa)
        {
            return _repository.GetAll(pesquisa);
        }

        public InfracaoModel GetById(int id, string usuario)
        {
            return _repository.GetById(id, usuario);
        }

        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            return _repository.GetTotalDeRegistros(pesquisa);
        }
    }
}