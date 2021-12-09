using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterAttributeCore.ResourceFilters
{
    public class CacheResourceFilter : IResourceFilter
    {
        private static readonly Dictionary<string, object> _cache
                = new Dictionary<string, object>();
        private string _cacheKey;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.ContainsKey(_cacheKey))
            {
                if (_cache[_cacheKey] is string cachedValue)
                {
                    context.Result = new ContentResult()
                    { Content = cachedValue };
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!String.IsNullOrEmpty(_cacheKey) && !_cache.ContainsKey(_cacheKey))
            {
                var result = context.Result;
                if (result != null)
                {
                    _cache.Add(_cacheKey, result.ToString());
                }
            }
        }
    }
}
