using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public static class RestApiHelper
    {
        public static async Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> headers = null, string dateTimeFormat = null)
        {
            CustomDateTimeConverter dateTimeConverter = new CustomDateTimeConverter(dateTimeFormat);

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                string response = string.Empty;
                if (headers != null)
                {
                    bool hasContentType = false;
                    foreach (var header in headers)
                    {
                        if (header.Key.ToLower() == "content-type")
                        {
                            hasContentType = true;
                        }
                        client.Headers.Add(header.Key, header.Value);
                    }

                    if (!hasContentType)
                    {
                        client.Headers.Add("Content-Type", "application/json");
                    }
                }
                else
                {
                    client.Headers.Add("Content-Type", "application/json");
                }

                try
                {
                    response = await client.DownloadStringTaskAsync(url);
                }
                catch (WebException webExceptionex)
                {
                    throw HandleApiWebException(webExceptionex);
                }

                return JsonConvert.DeserializeObject<TResponse>(response/*, dateTimeConverter*/);

            }
        }
        public static async Task<byte[]> DownloadAsync(string url, Dictionary<string, string> headers = null)
        {
            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.Headers.Add(header.Key, header.Value);
                    }
                }

                try
                {
                    return await client.DownloadDataTaskAsync(url);
                }
                catch (WebException webExceptionex)
                {
                    throw HandleApiWebException(webExceptionex);
                }
            }
        }

        public static async Task<TResponse> PostAsync<TResponse, TRequest>(string url, TRequest request,
            Dictionary<string, string> headers = null, string dateTimeFormat = null, bool enableCamelCase = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            if (enableCamelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            CustomDateTimeConverter dateTimeConverter = new CustomDateTimeConverter(dateTimeFormat);
            settings.Converters.Add(dateTimeConverter);

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                string response = string.Empty;
                string data = JsonConvert.SerializeObject(request, settings);

                if (headers != null)
                {
                    bool hasContentType = false;
                    foreach (var header in headers)
                    {
                        if (header.Key.ToLower() == "content-type")
                        {
                            hasContentType = true;
                        }
                        client.Headers.Add(header.Key, header.Value);
                    }

                    if (!hasContentType)
                    {
                        client.Headers.Add("Content-Type", "application/json");
                    }
                }
                else
                {
                    client.Headers.Add("Content-Type", "application/json");
                }

                try
                {
                    InitiateSSLTrust();
                    response = await client.UploadStringTaskAsync(url, data);
                }
                catch (WebException webExceptionex)
                {
                    throw HandleApiWebException(webExceptionex);
                }
                return JsonConvert.DeserializeObject<TResponse>(response/*, dateTimeConverter*/);
            }
        }


        public static TResponse PostString<TResponse>(string url, string request,
            Dictionary<string, string> headers = null, string dateTimeFormat = null, bool enableCamelCase = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            if (enableCamelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            CustomDateTimeConverter dateTimeConverter = new CustomDateTimeConverter(dateTimeFormat);
            settings.Converters.Add(dateTimeConverter);

            using (WebClient client = new WebClient { Encoding = Encoding.UTF8 })
            {
                string response = string.Empty;
                string data = request;

                if (headers != null)
                {
                    bool hasContentType = false;
                    foreach (var header in headers)
                    {
                        if (header.Key.ToLower() == "content-type")
                        {
                            hasContentType = true;
                        }
                        client.Headers.Add(header.Key, header.Value);
                    }

                    if (!hasContentType)
                    {
                        client.Headers.Add("Content-Type", "application/json");
                    }
                }
                else
                {
                    client.Headers.Add("Content-Type", "application/json");
                }

                try
                {
                    InitiateSSLTrust();
                    response = client.UploadString(url, data);
                }
                catch (WebException webExceptionex)
                {
                    throw HandleApiWebException(webExceptionex);
                }

                return JsonConvert.DeserializeObject<TResponse>(response/*, dateTimeConverter*/);
            }
        }

        /// <summary>
        /// this handler written beaded on the SSON ApiExceptionHandler and how we are throw the exception
        /// in Tahaluf.FIHS.SingleSignOn.API.Handlers.ApiExceptionHandler
        /// </summary>
        /// <param name="webException"></param>
        /// <returns></returns>
        private static Exception HandleApiWebException(WebException webException)
        {



            Exception exception = webException;
            if (webException?.Response is System.Net.HttpWebResponse webResponse)
            {
                if (webResponse?.StatusCode == HttpStatusCode.Unauthorized)
                    exception = new SecurityException();

                if (webResponse?.StatusCode == HttpStatusCode.Forbidden)
                    exception = new UnauthorizedAccessException();

            }

            return exception;
        }

        private class CustomDateTimeConverter : DateTimeConverterBase
        {
            private string _dateTimeFormat = "dd/MM/yyyy HH:mm";
            public CustomDateTimeConverter(string dateTimeFormat)
            {
                _dateTimeFormat = !string.IsNullOrEmpty(dateTimeFormat) ? dateTimeFormat : _dateTimeFormat;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                return reader.Value != null
                    ? DateTime.ParseExact(reader.Value.ToString(), _dateTimeFormat, CultureInfo.InvariantCulture)
                    : reader.Value;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(((DateTime)value).ToString(_dateTimeFormat));
            }
        }


        public static void InitiateSSLTrust()
        {
            try
            {
                //Change SSL checks so that all checks pass
                ServicePointManager.ServerCertificateValidationCallback =
                   new RemoteCertificateValidationCallback(
                        delegate
                        { return true; }
                    );
            }
            catch (Exception ex)
            {
            }
        }


    }
}