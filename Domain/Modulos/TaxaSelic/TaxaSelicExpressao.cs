using System;
using System.Linq.Expressions;

namespace Domain.Modulos.TaxaSelic
{
    public static class TaxaSelicExpressao
    {

        public static Expression<Func<TaxaSelicModel, bool>> SetWhere(string campo, string valor)
        {
            switch (campo.ToUpper())
            {
                case "ANO":
                    return (c => c.Ano.ToString().Contains(valor));
                default:
                    return (c => c.Id.ToString().Contains(valor));
            }
        }

    }

}
