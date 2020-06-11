using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Modulos.TaxaSelic.Command
{
    public class TaxaSelicDeleteCommand : Notifiable, ICommand 
    {
        public int Id { get; set; }
        
        public TaxaSelicDeleteCommand(){}

        public TaxaSelicDeleteCommand(int id)
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