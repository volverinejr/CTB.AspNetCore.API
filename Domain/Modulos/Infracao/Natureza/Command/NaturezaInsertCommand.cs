using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Natureza.Command
{
    public class NaturezaInsertCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }
        public int Ponto { get; set; }
        public float Valor { get; set; }
        public float PercentualDeDesconto { get; set; }

        public NaturezaInsertCommand() { }

        public NaturezaInsertCommand(string nome, int ponto, float valor, float percentualDeDesconto)
        {
            Nome = nome;
            Ponto = ponto;
            Valor = valor;
            PercentualDeDesconto = percentualDeDesconto;
        }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()

                    .IsNotNullOrWhiteSpace(Nome, "Nome", "Nome deve ser preenchido.")
                    .HasMinLen(Nome, 4, "Nome", "Nome deve ter mais de 4 caracteres")
                    .HasMaxLen(Nome, 50, "Nome", "Nome deve ter menos de 50 caracteres")

                    .IsGreaterOrEqualsThan(Ponto, 0, "Ponto", "Ponto  deve ser maior ou igual a 0")
                    .IsLowerOrEqualsThan(Ponto, 7, "Ponto", "Ponto  deve ser menor ou igual a 7")

                    .IsGreaterThan(Valor, 0, "Valor", "Valor deve ser positivo")

                    .AreEquals(PercentualDeDesconto, 20, "PercentualDeDesconto", "PercentualDeDesconto tem quer ser igual a 20")
            );
        }
    }
}