using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.DynamicsCRM.Actions.Http
{
    /// <summary>
    /// Implementation For Generic Http Service Class
    /// </summary>
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Http Client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<HttpService> _logger;

        /// <summary>
        /// Consturctor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        #region Client Configuration

        /// <summary>
        /// Extension method to add HttpClient using Startup Class
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpClientToServiceCollection(IServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>();
        }

        #endregion

        #region Request

        public Task<HttpResult<T>> GetObjectAsync<T>(string url, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<T>(HttpMethod.Get, url, null, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<string>> GetStringAsync(string url, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<string>(HttpMethod.Get, url, null, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<T>> PostJsonAndGetObject<T>(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<T>(HttpMethod.Post, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<string>> PostJsonAndGetString(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<string>(HttpMethod.Post, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<T>> PutJsonAndGetObject<T>(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<T>(HttpMethod.Put, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<string>> PutJsonAndGetString(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<string>(HttpMethod.Put, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<T>> DeleteJsonAndGetObject<T>(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<T>(HttpMethod.Delete, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<string>> DeleteJsonAndGetString(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<string>(HttpMethod.Delete, url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<T>> PatchJsonAndGetObject<T>(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<T>(new HttpMethod("PATCH"), url, requestBody, headers, isFormUrlEncodedContent);
        }

        public Task<HttpResult<string>> PatchJsonAndGetString(string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            return ProcessRequestAsync<string>(new HttpMethod("PATCH"), url, requestBody, headers, isFormUrlEncodedContent);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Process Request Asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="requestBody"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private async Task<HttpResult<T>> ProcessRequestAsync<T>(HttpMethod method, string url, object requestBody, Dictionary<string, string> headers = null, bool isFormUrlEncodedContent = false)
        {
            try
            {
                if (isFormUrlEncodedContent)
                {
                    List<KeyValuePair<string, string>> payload = requestBody as List<KeyValuePair<string, string>>;

                    using HttpRequestMessage request = CreateFormUrlEncodedContentRequest(method, url, headers, payload);
                    using HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

                    return await GetHttpResponseMessageFromHttpResponse<T>(response).ConfigureAwait(false);
                }

                using (HttpRequestMessage request = CreateRequest(method, url, headers, requestBody))
                {
                    using HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                    return await GetHttpResponseMessageFromHttpResponse<T>(response).ConfigureAwait(false);
                }
            }
            catch (HttpRequestException hre)
            {
                _logger.LogError($"Http Request Exception triggered for {url} ");

                throw GetException(
                   $"Http Request Exception triggered for {url}",
                   originalException: hre,
                   url,
                   requestBody,
                   typeof(T).FullName);
            }
            catch (HttpException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Http Request Exception triggered for {url} ");

                throw GetException(
                  $"Unexpected exeption when calling {url}",
                  originalException: ex,
                  url,
                  requestBody,
                  typeof(T).FullName);
            }
        }

        /// <summary>
        /// Get Http Response Message From Http Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static async Task<HttpResult<T>> GetHttpResponseMessageFromHttpResponse<T>(HttpResponseMessage response)
        {
            if (response is null) return null;

            var requestBody = response?.RequestMessage?.Content != null ? await response.RequestMessage.Content.ReadAsStringAsync().ConfigureAwait(false) : string.Empty;
            var responseBody = response?.Content != null ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : string.Empty;

            var httpResult = new HttpResult<T>
            {
                RequestUri = response.RequestMessage.RequestUri.ToString(),
                StatusCode = response.StatusCode,
                ResponseBody = responseBody,
                ReasonPhrase = response.ReasonPhrase,
                IsSuccessful = response.IsSuccessStatusCode,
            };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    // Only Serialize if return a specific type
                    if (typeof(T) != typeof(string))
                    {
                        var serializerSettings = new JsonSerializerSettings
                        {
                            StringEscapeHandling = StringEscapeHandling.EscapeHtml
                        };
                        httpResult.Result = JsonConvert.DeserializeObject<T>(responseBody, serializerSettings);
                    }
                    else
                    {
                        httpResult.Result = (T)Convert.ChangeType(responseBody, typeof(T));
                    }
                }
                else
                {
                    httpResult.Message = $"Call to {response.RequestMessage.RequestUri} failed with code {response.StatusCode}. Reason: {response.ReasonPhrase}";
                }

                return httpResult;
            }
            catch (Exception ex)
            {
                throw GetException(
                    $"[GetHttpResponseMessageFromHttpResponse] Error reading response from {response.RequestMessage.RequestUri} into {typeof(T).FullName}",
                    originalException: ex,
                    response.RequestMessage.RequestUri.ToString(),
                    requestBody,
                    typeof(T).FullName,
                    responseBody);
            }
        }

        /// <summary>
        /// Get Exception of different parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="originalException"></param>
        /// <param name="requestUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="expectedResponseTypeName"></param>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        private static HttpException GetException(string message, Exception originalException, string requestUri, object requestBody, string expectedResponseTypeName, string responseBody = "")
        {
            string requestBodyString;

            try
            {
                requestBodyString = JsonConvert.SerializeObject(requestBody);
            }
            catch
            {
                requestBodyString = "Cannot serialize requestBody";
            }

            return new HttpException(message, originalException)
            {
                RequestUri = requestUri,
                RequestBody = requestBodyString,
                ResponseBody = responseBody,
                ExpectedResponseTypeName = expectedResponseTypeName
            };
        }

        /// <summary>
        /// Create Http Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static HttpRequestMessage CreateRequest(HttpMethod method, string url, Dictionary<string, string> headers, object body)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };

            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            if (body != null)
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                };

                var jsonBody = JsonConvert.SerializeObject(body, serializerSettings);

                request.Content = method.Method == "PATCH"
                    ? new StringContent(jsonBody, Encoding.UTF8, "application/merge-patch+json")
                    : new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            return request;
        }

        /// <summary>
        /// Create Form Url Encoded Content Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static HttpRequestMessage CreateFormUrlEncodedContentRequest(HttpMethod method, string url, Dictionary<string, string> headers, List<KeyValuePair<string, string>> body)
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };

            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            if (body != null)
            {
                request.Content = new FormUrlEncodedContent(body);
            }

            return request;
        }
        #endregion
    }
}
