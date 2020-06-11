using System;
using System.Linq.Expressions;

namespace Domain.Modulos.Infracao.Infracao
{
    public static class InfracaoExpressao
    {
        public static Expression<Func<InfracaoModel, bool>> SetWhere(string campo, string valor)
        {
            switch (campo.ToUpper())
            {
                case "CODIGO":
                    return (c => c.Codigo.ToUpper().Contains(valor.Trim().ToUpper()));
                case "DESCRICAO":
                    return (c => c.Descricao.ToUpper().Contains(valor.Trim().ToUpper()));
                case "AMPAROLEGAL":
                    return (c => c.AmparoLegal.ToUpper().Contains(valor.Trim().ToUpper()));
                case "GRUPOID":
                    return (c => c.GrupoId.ToString().Contains(valor));
                case "NATUREZAID":
                    return (c => c.NaturezaId.ToString().Contains(valor));
                default:
                    return (c => c.Id.ToString().Contains(valor));
            }
        }        
        
    }
}