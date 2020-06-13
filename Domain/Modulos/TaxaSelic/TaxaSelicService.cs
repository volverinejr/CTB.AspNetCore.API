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
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            TaxaSelicModel model = _repository.GetByAnoeMes(command.Ano, command.Mes);
            if (model != null)
            {
                return new GenericResult(409, "Recurso já existente", model);
            }

            model = new TaxaSelicModel(command.Ano, command.Mes, command.Valor);

            try
            {
                _repository.Insert(model);

                return new GenericResult(201, "Recurso Inserido(a)", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }

        public GenericResult Exec(TaxaSelicUpdateCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            TaxaSelicModel model = _repository.GetById(command.Id);

            if (model == null)
            {
                return new GenericResult(404, "Recurso Inexistente", null);
            }

            model.Atualizar(command.Ano, command.Mes, command.Valor);

            try
            {
                _repository.Update(model);

                return new GenericResult(200, "Recurso Atualizado(a)", model);
            }
            catch (System.Exception ex)
            {
                return new GenericResult(503, ex.GetBaseException().Message, null);
            }
        }

        public GenericResult Exec(TaxaSelicDeleteCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            TaxaSelicModel model = _repository.GetById(command.Id);

            if (model == null)
            {
                return new GenericResult(404, "Recurso Inexistente", null);
            }

            try
            {
                _repository.Delete(model);

                return new GenericResult(204, "Recurso Removido(a)", null);
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

        public int GetTotalDeRegistros()
        {
            return _repository.GetTotalDeRegistros();
        }

    }
}