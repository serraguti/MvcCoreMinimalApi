using MvcCoreMinimalApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreMinimalApi.Services
{
    public class ServiceMinimalApiPersonajes
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceMinimalApiPersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiMinimalPersonajes");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODO GENERICO PARA RECUPERAR DATOS
        private async Task<T> CallGetApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = 
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallGetSecureApiAsync<T>
            (string request, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //DEBEMOS INCLUIR LAS CREDENCIALES EN LA CABECERA Authorization
                string credentials = username + ":" + password;
                //CONVERTIMOS LAS CREDENCIALES A BYTES
                byte[] credentialBytes = Encoding.UTF8.GetBytes(credentials);
                string token = Convert.ToBase64String(credentialBytes);
                //AÑADIMOS LAS CREDENCIALES EN EL REQUEST
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task CallPostAsync<T>(string request, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json =
                    JsonConvert.SerializeObject(data);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync
            (string username, string password)
        {
            string request = "/personajes";
            List<Personaje> personajes =
                await this.CallGetSecureApiAsync<List<Personaje>>(request, username, password);
            return personajes;
        }

        public async Task<List<Personaje>> FindPersonajesSerieAsync(int idserie)
        {
            string request = "/personajes/serie/" + idserie;
            List<Personaje> personajes =
                await this.CallGetApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task AddPersonajeAsync(Personaje personaje)
        {
            string request = "/personajes/post";
            await this.CallPostAsync<Personaje>(request, personaje);
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/series";
            List<Serie> series =
                await this.CallGetApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task AddSerieAsync(Serie serie)
        {
            string request = "/series/post";
            await this.CallPostAsync<Serie>(request, serie);
        }
    }
}
