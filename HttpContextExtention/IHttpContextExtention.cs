using HttpContextExtention.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpContextExtention
{
    //*---------------------------------------------------------------------------*//
    //* CREATOR          : TRẦN HỮU HẢI NAM ( WT436 )                             *//
    //* DATE_CREATE      : Monday, March 18, 2021                                 *//
    //* DATE_UPDATE      : Friday, August 2, 2022                                 *//
    //* REFERENCE_SOURCE : GOOGLE                                                 *//
    //* DESCRIPTION      : SUPPORT HTTPCONTEXT                                    *//
    //*---------------------------------------------------------------------------*//
    public interface IHttpContextExtention
    {
        /// <summary>
        /// Basic for HttpGet no Headers return class
        /// </summary>
        Task<T> GetAsync<T>(string uri) where T : class, new();
        /// <summary>
        /// Basic for HttpGet no Headers return string json
        /// </summary>
        Task<string> GetAsync(string uri);
        /// <summary>
        /// HttpGet only Headers Token return class
        /// </summary>
        Task<T> GetAsync<T>(string uri, IEnumerable<TokenConfig> tokenConfigs) where T : class, new();
        /// <summary>
        /// HttpGet only Headers Token return string json
        /// </summary>
        Task<string> GetAsync(string uri, IEnumerable<TokenConfig> tokenConfigs);
        /// <summary>
        /// HttpGet full Headers Token return class
        /// </summary>
        Task<T> GetAsync<T>(string uri, Headers headers) where T : class, new();
        /// <summary>
        /// HttpGet full Headers Token return string json
        /// </summary>
        Task<string> GetAsync(string uri, Headers headers);
    }
}
