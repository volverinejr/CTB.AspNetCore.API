using System;
using System.Linq.Expressions;

namespace Domain.Modulos.Infracao.Natureza
{
    public static class NaturezaExpressao
    {
        public static Expression<Func<NaturezaModel, bool>> SetWhere(string campo, string valor)
        {
            switch (campo.ToUpper())
            {
                case "NOME":
                    return (c => c.Nome.Trim().ToUpper().Contains(valor.Trim().ToUpper()));
                case "VALOR":
                    return (c => c.Valor.ToString().Contains(valor.Trim()));
                default:
                    return (c => c.Id.ToString().Contains(valor.Trim()));
            }
        }

    }
}