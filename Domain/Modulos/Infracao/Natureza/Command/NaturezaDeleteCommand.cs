using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Modulos.Infracao.Natureza.Command
{
    public class NaturezaDeleteCommand : Notifiable, ICommand
    {
        public int Id { get; set; }

        public NaturezaDeleteCommand() { }

        public NaturezaDeleteCommand(int id)
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