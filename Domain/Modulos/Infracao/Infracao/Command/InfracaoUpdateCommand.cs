using System;
using Domain.Shared.Contratos;
using Flunt.Notifications;
using Flunt.Validations;


namespace Domain.Modulos.Infracao.Infracao.Command
{
    public class InfracaoUpdateCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string AmparoLegal { get; set; }
        public string MedidaAdm { get; set; }
        public bool Crime { get; set; }
        public bool ObsObrigatoria { get; set; }
        public int GrupoId { get; set; }
        public int NaturezaId { get; set; }
        public bool CompetenciaMunicipal { get; set; }
        public bool CompetenciaEstadual { get; set; }
        public bool CompetenciaRodoviaria { get; set; }
        public bool InfratorCondutor { get; set; }
        public bool InfratorProprietario { get; set; }
        public DateTime ValidadeInicio { get; set; }


        public InfracaoUpdateCommand() { }

        public InfracaoUpdateCommand(int id, string codigo, string descricao, string amparoLegal, string medidaAdm, bool crime, bool obsObrigatoria, int grupoId, int naturezaId, bool competenciaMunicipal, bool competenciaEstadual, bool competenciaRodoviaria, bool infratorCondutor, bool infratorProprietario, DateTime validadeInicio)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            AmparoLegal = amparoLegal;
            MedidaAdm = medidaAdm;
            Crime = crime;
            ObsObrigatoria = obsObrigatoria;
            GrupoId = grupoId;
            NaturezaId = naturezaId;
            CompetenciaMunicipal = competenciaMunicipal;
            CompetenciaEstadual = competenciaEstadual;
            CompetenciaRodoviaria = competenciaRodoviaria;
            InfratorCondutor = infratorCondutor;
            InfratorProprietario = infratorProprietario;
            ValidadeInicio = validadeInicio;
        }

        public void Validate()
        {
            if ((MedidaAdm != null) && (MedidaAdm.Length > 100))
            {
                AddNotification("MedidaAdm", "MedidaAdm deve ter menos de 100 caracteres");
            }

            bool validarCompetencia = (CompetenciaMunicipal || CompetenciaEstadual || CompetenciaRodoviaria) ? true : false;
            bool validarInfrator = (InfratorCondutor || InfratorProprietario) ? true : false;

            AddNotifications(
                new Contract()
                    .Requires()

                    .IsGreaterThan(Id, 0, "Id", "Código inválido")

                    .IsNotNullOrWhiteSpace(Codigo, "Nome", "Nome deve ser preenchido.")
                    .HasLen(Codigo, 5, "Codigo", "Codigo deve ter 5 caracteres")

                    .IsNotNullOrWhiteSpace(Descricao, "Nome", "Nome deve ser preenchido.")
                    .HasMinLen(Descricao, 10, "Descricao", "Descricao deve ter mais de 10 caracteres")
                    .HasMaxLen(Descricao, 500, "Descricao", "Descricao deve ter menos de 500 caracteres")

                    .IsNotNullOrWhiteSpace(AmparoLegal, "Nome", "Nome deve ser preenchido.")
                    .HasMinLen(AmparoLegal, 5, "AmparoLegal", "AmparoLegal deve ter mais de 5 caracteres")
                    .HasMaxLen(AmparoLegal, 100, "AmparoLegal", "AmparoLegal deve ter menos de 100 caracteres")

                    .IsGreaterThan(GrupoId, 0, "GrupoId", "GrupoId inválido")

                    .IsGreaterThan(NaturezaId, 0, "NaturezaId", "NaturezaId inválido")

                    .IsGreaterThan(ValidadeInicio, new DateTime(2000, 1, 1, 0, 0, 0), "ValidadeInicio", "Data de Inicio da Validade inválida")

                    .IsTrue(validarCompetencia, "Competência", "Informe ao menos uma Competência")

                    .IsTrue(validarInfrator, "Infrator", "Informe ao menos um Infrator")
            );
        }
    }
}