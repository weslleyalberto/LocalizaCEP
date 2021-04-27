using AppleTestes.Interfaces;
using AppleTestes.iOS.Services;
using AppleTestes.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
[assembly: Xamarin.Forms.Dependency(typeof(ServiceAPIIos))]
namespace AppleTestes.iOS.Services
{
    public class ServiceAPIIos : IServiceLocalizaEndereco<Endereco>
    {
        public async Task<Endereco> BuscarEnderecoAsync(string cep)
        {
            Uri baseUrl = new Uri($"https://viacep.com.br/ws/{cep}/json/");
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await http.GetAsync(baseUrl);
                    response.EnsureSuccessStatusCode();
                    return JsonSerializer.Deserialize<Endereco>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error: {ex.Message}");
                }
                
            }
               
        }
    }
}