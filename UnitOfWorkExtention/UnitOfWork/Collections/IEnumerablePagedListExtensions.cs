using System;
using System.Collections.Generic;

namespace UnitOfWork.Collections
{
    public static class IEnumerablePagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 0) => new PagedList<T>(source, pageIndex, pageSize, indexFrom);
        public static IPagedList<T> MapToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize,int totalCount , int indexFrom) => new PagedList<T>(source, pageIndex, pageSize, totalCount, indexFrom);
        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 0) => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
    }
}
