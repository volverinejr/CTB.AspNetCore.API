using System.Collections.Generic;
using System.Linq;

namespace Domain.Shared.Classes
{
    public static class Ordenacao
    {
        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source, string propertyName, int Ascending)
        {
            if (Ascending ==  1)
                return source.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
            else
                return source.OrderByDescending(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }        
        
    }
}