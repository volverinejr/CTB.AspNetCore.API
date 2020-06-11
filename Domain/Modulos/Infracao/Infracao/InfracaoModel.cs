using System;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Natureza;

namespace Domain.Modulos.Infracao.Infracao
{
    public class InfracaoModel
    {

        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string AmparoLegal { get; private set; }
        public string MedidaAdm { get; private set; }
        public bool Crime { get; private set; }
        public bool ObsObrigatoria { get; private set; }
        public GrupoModel Grupo { get; private set; }
        public int GrupoId { get; private set; }
        public NaturezaModel Natureza { get; private set; }
        public int NaturezaId { get; private set; }
        public bool CompetenciaMunicipal { get; private set; }
        public bool CompetenciaEstadual { get; private set; }
        public bool CompetenciaRodoviaria { get; private set; }
        public bool InfratorCondutor { get; private set; }
        public bool InfratorProprietario { get; private set; }
        public DateTime ValidadeInicio { get; private set; }
        public DateTime ValidadeFim { get; private set; }
        public DateTime DataAtualizacao { get; private set; }


        public InfracaoModel(string codigo, string descricao, string amparoLegal, string medidaAdm, bool crime, bool obsObrigatoria, int grupoId, int naturezaId, bool competenciaMunicipal, bool competenciaEstadual, bool competenciaRodoviaria, bool infratorCondutor, bool infratorProprietario, DateTime validadeInicio)
        {
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
            DataAtualizacao = DateTime.Now;
        }

        public void Atualizar(string codigo, string descricao, string amparoLegal, string medidaAdm, bool crime, bool obsObrigatoria, int grupoId, int naturezaId, bool competenciaMunicipal, bool competenciaEstadual, bool competenciaRodoviaria, bool infratorCondutor, bool infratorProprietario, DateTime validadeInicio)
        {
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
            DataAtualizacao = DateTime.Now;
        }


        public void SetDataFim(DateTime validadeFim)
        {
            ValidadeFim = validadeFim;
        }

    }
}