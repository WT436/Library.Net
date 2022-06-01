using HttpContextExtention.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpContextExtention
{
    public class HttpContextExtention : IHttpContextExtention
    {
        private const string MediaType = "application/json";
        private readonly HttpClient httpClient;
        private HttpRequestMessage httpRequestMessage;

        public HttpContextExtention(string host)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(host),
                Timeout = TimeSpan.FromMilliseconds(100000)
            };
        }

        #region HttpGet
        public async Task<T> GetAsync<T>(string uri) where T : class, new()
        {
            T result = new T();

            var response = await httpClient.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            response.EnsureSuccessStatusCode();

            JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await httpClient.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException(await response.Content.ReadAsStringAsync());
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(string uri, IEnumerable<TokenConfig> tokenConfigs) where T : class, new()
        {
            T result = new T();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                if (tokenConfigs != null || tokenConfigs.Count() != 0)
                {
                    foreach (var token in tokenConfigs)
                    {
                        if (string.IsNullOrEmpty(token.KeyToken) || string.IsNullOrEmpty(token.ValuesToken))
                        {
                            throw new HttpRequestException("There is an empty token!");
                        }

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(token.KeyToken, token.ValuesToken);
                    }
                }
                else
                {
                    throw new HttpRequestException("TokenConfigs is not null!");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

                var response = await httpClient.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                response.EnsureSuccessStatusCode();
                JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetAsync(string uri, IEnumerable<TokenConfig> tokenConfigs)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                if (tokenConfigs != null || tokenConfigs.Count() != 0)
                {
                    foreach (var token in tokenConfigs)
                    {
                        if (string.IsNullOrEmpty(token.KeyToken) || string.IsNullOrEmpty(token.ValuesToken))
                        {
                            throw new HttpRequestException("There is an empty token!");
                        }

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(token.KeyToken, token.ValuesToken);
                    }
                }
                else
                {
                    throw new HttpRequestException("TokenConfigs is not null!");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

                var response = await httpClient.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetAsync<T>(string uri, Headers headers) where T : class, new()
        {
            T result = new T();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                if (headers != null)
                {
                    if (headers.TokenConfigs != null || headers.TokenConfigs.Count() != 0)
                    {
                        foreach (var token in headers.TokenConfigs)
                        {
                            if (string.IsNullOrEmpty(token.KeyToken) || string.IsNullOrEmpty(token.ValuesToken))
                            {
                                throw new HttpRequestException("There is an empty token!");
                            }

                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(token.KeyToken, token.ValuesToken);
                        }
                    }
                    else
                    {
                        throw new HttpRequestException("TokenConfigs is not null!");
                    };

                    if (headers.HeadersConfigs != null || headers.HeadersConfigs.Count() != 0)
                    {
                        foreach (var header in headers.HeadersConfigs)
                        {
                            if (string.IsNullOrEmpty(header.KeyHeaders) || string.IsNullOrEmpty(header.ValuesHeaders))
                            {
                                throw new HttpRequestException("There is an empty token!");
                            }

                            httpClient.DefaultRequestHeaders.Add(header.KeyHeaders, header.ValuesHeaders);
                        }
                    }
                    else
                    {
                        throw new HttpRequestException("HeadersConfigs is not null!");
                    }
                }
                else
                {
                    throw new HttpRequestException("Headers is not null!");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

                var response = await httpClient.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                response.EnsureSuccessStatusCode();
                JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetAsync(string uri, Headers headers)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                if (headers != null)
                {
                    if (headers.TokenConfigs != null || headers.TokenConfigs.Count() != 0)
                    {
                        foreach (var token in headers.TokenConfigs)
                        {
                            if (string.IsNullOrEmpty(token.KeyToken) || string.IsNullOrEmpty(token.ValuesToken))
                            {
                                throw new HttpRequestException("There is an empty token!");
                            }

                            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(token.KeyToken, token.ValuesToken);
                        }
                    }
                    else
                    {
                        throw new HttpRequestException("tokenConfigs is not null!");
                    };

                    if (headers.HeadersConfigs != null || headers.HeadersConfigs.Count() != 0)
                    {
                        foreach (var header in headers.HeadersConfigs)
                        {
                            if (string.IsNullOrEmpty(header.KeyHeaders) || string.IsNullOrEmpty(header.ValuesHeaders))
                            {
                                throw new HttpRequestException("There is an empty token!");
                            }

                            httpClient.DefaultRequestHeaders.Add(header.KeyHeaders, header.ValuesHeaders);
                        }
                    }
                    else
                    {
                        throw new HttpRequestException("HeadersConfigs is not null!");
                    }
                }
                else
                {
                    throw new HttpRequestException("Headers is not null!");
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));

                var response = await httpClient.GetAsync(uri);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region HttpPost

        #endregion

        #region Common
        //public HttpContextExtention(CustomHeaderConfig customHeaderConfig)
        //{
        //    httpRequestMessage = new HttpRequestMessage
        //    {
        //        RequestUri = new Uri("[your request url string]"),
        //        Method = HttpMethod.Post,
        //        Headers = {
        //            { "X-Version", "1" }, // HERE IS HOW TO ADD HEADERS,
        //            { HttpRequestHeader.Authorization.ToString(), "[your authorization token]" },
        //            { HttpRequestHeader.ContentType.ToString(), "multipart/mixed" },//use this content type if you want to send more than one content type
        //        },
        //        Content = new MultipartContent { // Just example of request sending multipart request
        //                                new ObjectContent<[YOUR JSON OBJECT TYPE]>(
        //                                new [YOUR JSON OBJECT TYPE INSTANCE](...){...},
        //                                new JsonMediaTypeFormatter(),
        //                                "application/json"), // this will add 'Content-Type' header for the first part of request
        //                                new ByteArrayContent([BINARY DATA]) {
        //                                             HttpRequestMessage.Headers = {
        //                                             { "Content-Type", "application/Executable" },
        //                                             { "Content-Disposition", "form-data; filename=\"test.pdf\"" }
        //                                },
        //        },
        //    },
        //};
        //}

        public async Task<T> CommonHttpCustomHeader<T>() where T : class, new()
        {
            return new T();
        }
    }
    #endregion
}
