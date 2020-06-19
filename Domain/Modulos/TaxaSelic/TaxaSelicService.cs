using System;
using System.Collections.Generic;
using Domain.Modulos.TaxaSelic.Command;
using Domain.Shared.Contratos;
using Domain.Shared.Entidade;
using Flunt.Notifications;

namespace Domain.Modulos.TaxaSelic
{
    public class TaxaSelicService : Notifiable,
        IExec<TaxaSelicInsertCommand>,
        IExec<TaxaSelicUpdateCommand>,
        IExec<TaxaSelicDeleteCommand>
    {
        private readonly ITaxaSelicRepository _repository;

        public TaxaSelicService(ITaxaSelicRepository repository)
        {
            _repository = repository;
        }


        public GenericResult Exec(TaxaSelicInsertCommand command)
        {
            TaxaSelicModel model = _repository.GetByAnoeMes(command.Ano, command.Mes);
            if (model != null)
            {
                command.AddNotification("TaxaSelicModel", "Recurso já existente Id: " + model.Id);
            }

            command.Validate();
            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido", command.Notifications);
            }


            model = new TaxaSelicModel(command.Ano, command.Mes, command.Valor);
            try
            {
                _repository.Insert(model);

                return new GenericResult(201, "Recurso Inserido", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }

        public GenericResult Exec(TaxaSelicUpdateCommand command)
        {
            TaxaSelicModel model = _repository.GetByAnoeMes(command.Ano, command.Mes);
            if ( (model != null) && ( model.Id != command.Id ) )
            {
                command.AddNotification("TaxaSelicModel", "Recurso já existente Id: " + model.Id);
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


            model.Atualizar(command.Ano, command.Mes, command.Valor);

            try
            {
                _repository.Update(model);

                return new GenericResult(200, "Recurso Atualizado", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }

        public GenericResult Exec(TaxaSelicDeleteCommand command)
        {
            TaxaSelicModel model = _repository.GetById(command.Id);
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


        public IEnumerable<TaxaSelicModel> GetAll(Pesquisa pesquisa)
        {
            return _repository.GetAll(pesquisa);
        }

        public TaxaSelicModel GetById(int id, string usuario)
        {
            return _repository.GetById(id, usuario);
        }

        public int GetTotalDeRegistros(Pesquisa pesquisa)
        {
            return _repository.GetTotalDeRegistros(pesquisa);
        }

    }
}