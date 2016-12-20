using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syns.Ticket.Business
{
    public static class PagingExtensions
    {
        //used by LINQ to SQL
        public static List<TSource> Page<TSource>(this IQueryable<TSource> source, int startIndex, int pageSize, out int count)
        {
            count = source.Count();

            return source.Skip(startIndex).Take(pageSize).ToList();

        }

        //used by LINQ
        public static List<TSource> Page<TSource>(this IEnumerable<TSource> source, int startIndex, int pageSize, out int count)
        {
            count = source.Count();

            return source.Skip(startIndex).Take(pageSize).ToList();
        }
    }
}
