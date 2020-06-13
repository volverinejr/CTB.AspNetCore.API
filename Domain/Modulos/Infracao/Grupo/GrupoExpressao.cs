using System;
using System.Linq.Expressions;

namespace Domain.Modulos.Infracao.Grupo
{
    public static class GrupoExpressao
    {
        public static Expression<Func<GrupoModel, bool>> SetWhere(string campo, string valor)
        {
            switch (campo.ToUpper())
            {
                case "NOME":
                    return (c => c.Nome.Trim().ToUpper().Contains(valor.Trim().ToUpper()));
                default:
                    return (c => c.Id.ToString().Contains(valor));
            }
        }

    }
}