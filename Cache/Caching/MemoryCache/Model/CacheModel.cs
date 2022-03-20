using System;

namespace Cache.Model
{
    public class CacheModel
    {
        public string Key {get; set;}
        public int CacheLoad {get; set;}
        public DateTime TimeCacheInit {get; set;}
    }
}