using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Grupo.Command
{
    public class GrupoUpdateCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public GrupoUpdateCommand() { }

        public GrupoUpdateCommand(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }


        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()

                    .IsGreaterThan(Id, 0, "Id", "Código inválido")

                    .IsNotNullOrWhiteSpace(Nome, "Nome", "Nome deve ser preenchido.")
                    .HasMinLen(Nome, 4, "Nome", "Nome deve ter mais de 4 caracteres")
                    .HasMaxLen(Nome, 50, "Nome", "Nome deve ter menos de 50 caracteres")
            );
        }
    }
}