using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace ABCRetailers.Functions.Helpers
{
    public static class HttpJson
    {
        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public static async Task<T?> ReadAsync<T>(HttpRequestData req)
        {
            using var stream = req.Body;
            return await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);
        }

        public static Task<HttpResponseData> Ok<T>(HttpRequestData req, T body)
        {
            return WriteAsync(req, HttpStatusCode.OK, body);
        }

        public static Task<HttpResponseData> Created<T>(HttpRequestData req, T body)
        {
            return WriteAsync(req, HttpStatusCode.Created, body);
        }

        public static Task<HttpResponseData> Bad(HttpRequestData req, string message)
        {
            return TextAsync(req, HttpStatusCode.BadRequest, message);
        }

        public static Task<HttpResponseData> NotFound(HttpRequestData req, string message = "Not Found")
        {
            return TextAsync(req, HttpStatusCode.NotFound, message);
        }

        public static Task<HttpResponseData> NoContentAsync(HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.NoContent);
            return Task.FromResult(response);
        }

        public static async Task<HttpResponseData> TextAsync(HttpRequestData req, HttpStatusCode code, string message)
        {
            var response = req.CreateResponse(code);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await response.WriteStringAsync(message, Encoding.UTF8);
            return response;
        }

        private static async Task<HttpResponseData> WriteAsync<T>(HttpRequestData req, HttpStatusCode code, T body)
        {
            var response = req.CreateResponse(code);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var json = JsonSerializer.Serialize(body, _jsonOptions);
            await response.WriteStringAsync(json, Encoding.UTF8);
            return response;
        }
    }
}
