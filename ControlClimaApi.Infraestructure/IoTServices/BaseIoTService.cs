using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Infraestructure.IoTServices
{
    public class BaseIoTService
    {
        private const string UrlPrincipal = "http://192.168.1.22/";

        protected IHttpClientFactory _httpClientFactory;

        public BaseIoTService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected HttpClient GetClient()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(UrlPrincipal);
            // client.DefaultRequestHeaders.Add("Authorization", "Bearer abc"); Se aplicará cuando se agregue una capa de autorización

            return client;
        }

        protected HttpClient GetPostClient()
        {
            HttpClient client = GetClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
    }
}
