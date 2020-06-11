using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Grupo.Command
{
    public class GrupoInsertCommand : Notifiable, ICommand
    {
        public string Nome { get; set; }

        public GrupoInsertCommand() { }

        public GrupoInsertCommand(string nome)
        {
            Nome = nome;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()

                    .IsNotNullOrWhiteSpace(Nome, "Nome", "Nome deve ser preenchido.")

                    .HasMinLen(Nome, 4, "Nome", "Nome deve ter mais de 4 caracteres")
                    .HasMaxLen(Nome, 50, "Nome", "Nome deve ter menos de 50 caracteres")
            );
        }
    }
}