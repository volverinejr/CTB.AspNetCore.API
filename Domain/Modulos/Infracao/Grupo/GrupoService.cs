using System.Collections.Generic;
using Domain.Modulos.Infracao.Grupo.Command;
using Domain.Shared.Contratos;
using Domain.Shared.Entidade;
using Flunt.Notifications;


namespace Domain.Modulos.Infracao.Grupo
{
    public class GrupoService: Notifiable,
        IExec<GrupoInsertCommand>,
        IExec<GrupoUpdateCommand>,
        IExec<GrupoDeleteCommand>
    {

        private readonly IGrupoRepository _repository;

        public GrupoService(IGrupoRepository repository)
        {
            _repository = repository;
        }


        public GenericResult Exec(GrupoInsertCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            GrupoModel model = new GrupoModel(command.Nome);

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

        public GenericResult Exec(GrupoUpdateCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            GrupoModel model = _repository.GetById(command.Id);

            if (model == null)
            {
                return new GenericResult(404, "Recurso Inexistente", null);
            }

            model.Atualizar(command.Nome);

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

        public GenericResult Exec(GrupoDeleteCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericResult(400, "Recurso Inválido(a)", command.Notifications);
            }

            GrupoModel model = _repository.GetById(command.Id);

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


        public IEnumerable<GrupoModel> GetAll(Pesquisa pesquisa)
        {
            return _repository.GetAll(pesquisa);
        }

        public GrupoModel GetById(int id, string usuario)
        {
            return _repository.GetById(id, usuario);
        }

        public int GetTotalDeRegistros()
        {
            return _repository.GetTotalDeRegistros();
        }

         
    }
}