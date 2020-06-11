using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;

namespace Domain.Modulos.TaxaSelic.Command
{
    public class TaxaSelicUpdateCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public float Valor { get; set; }



        public TaxaSelicUpdateCommand(){}

        public TaxaSelicUpdateCommand(int id, int ano, int mes, float valor)
        {
            Id = id;
            Ano = ano;
            Mes = mes;
            Valor = valor;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()

                    .IsGreaterThan(Id, 0, "Id", "Código inválido")

                    .IsGreaterThan(Ano, 2015, "Ano", "Ano deve ser maior que 2015")

                    .IsGreaterOrEqualsThan(Mes, 1, "Mes", "Mês deve estar entre 1 e 12")
                    .IsLowerOrEqualsThan(Mes, 12, "Mes", "Mês deve estar entre 1 e 12")

                    .IsGreaterThan(Valor, 0, "Valor", "Valor deve ser positivo")
            );
        }
    }
}