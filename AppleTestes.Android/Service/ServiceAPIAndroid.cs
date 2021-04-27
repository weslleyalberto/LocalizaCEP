using AppleTestes.Droid.Service;
using AppleTestes.Interfaces;
using AppleTestes.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
[assembly:Xamarin.Forms.Dependency(typeof(ServiceAPIAndroid))]
namespace AppleTestes.Droid.Service
{
    public class ServiceAPIAndroid : IServiceLocalizaEndereco<Endereco>
    {
        public async Task<Endereco> BuscarEnderecoAsync(string cep)
        {
            Uri baseUrl = new Uri($"https://viacep.com.br/ws/{cep}/json/");
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    HttpResponseMessage response = await http.GetAsync(baseUrl);
                    response.EnsureSuccessStatusCode();
                    return JsonSerializer.Deserialize<Endereco>(await response.Content.ReadAsStringAsync());
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }
    }
}