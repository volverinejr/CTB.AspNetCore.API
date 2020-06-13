using System.Collections.Generic;
using Domain.Modulos.Infracao.Natureza.Command;
using Domain.Shared.Contratos;
using Domain.Shared.Entidade;
using Flunt.Notifications;

namespace Domain.Modulos.Infracao.Natureza
{
    public class NaturezaService : Notifiable,
        IExec<NaturezaInsertCommand>,
        IExec<NaturezaUpdateCommand>,
        IExec<NaturezaDeleteCommand>
    {

        private readonly INaturezaRepository _repository;

        public NaturezaService(INaturezaRepository repository)
        {
            _repository = repository;
        }


        public GenericResult Exec(NaturezaInsertCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            NaturezaModel model = new NaturezaModel(command.Nome, command.Ponto, command.Valor, command.PercentualDeDesconto);

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

        public GenericResult Exec(NaturezaUpdateCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            NaturezaModel model = _repository.GetById(command.Id);

            if (model == null)
            {
                return new GenericResult(404, "Recurso Inexistente", null);
            }

            model.Atualizar(command.Nome, command.Ponto, command.Valor, command.PercentualDeDesconto);

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

        public GenericResult Exec(NaturezaDeleteCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            NaturezaModel model = _repository.GetById(command.Id);

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


        public IEnumerable<NaturezaModel> GetAll(Pesquisa pesquisa)
        {
            return _repository.GetAll(pesquisa);
        }

        public NaturezaModel GetById(int id, string usuario)
        {
            return _repository.GetById(id, usuario);
        }

        public int GetTotalDeRegistros()
        {
            return _repository.GetTotalDeRegistros();
        }

         
    }
}