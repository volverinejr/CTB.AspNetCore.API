using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Infracao.Command
{
    public class InfracaoDeleteCommand : Notifiable, ICommand
    {
        public int Id { get; set; }

        public InfracaoDeleteCommand() { }

        public InfracaoDeleteCommand(int id)
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