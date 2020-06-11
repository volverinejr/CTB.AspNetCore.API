using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Grupo.Command
{
    public class GrupoDeleteCommand : Notifiable, ICommand
    {
        public int Id { get; set; }

        public GrupoDeleteCommand() { }

        public GrupoDeleteCommand(int id)
        {
            Id = id;
        }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()

                    .IsGreaterThan(Id, 0, "Id", "Código inválido")
            );
        }

    }
}