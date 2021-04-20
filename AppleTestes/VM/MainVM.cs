using AppleTestes.Interfaces;
using AppleTestes.Models;
using AppleTestes.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AppleTestes.VM
{
    [Preserve(AllMembers = true)]
    public class MainVM : Endereco
    {
        public ICommand BuscarEndereco { get; set; }
        public ICommand BtnLimparCampo { get; set; }
        private bool _estaAtivo;

        public bool EstaAtivo
        {
            get { return _estaAtivo; }
            set {SetProperty(ref _estaAtivo , value); }
        }

        private IServiceLocalizaEndereco<Endereco> _service;
       



        public MainVM()
        {
            EstaAtivo = false;
            BuscarEndereco = new Command(async () => await LocalizaEnderecoCEPAsync());          
            _service = new ServiceLocalizaEndereco();
            BtnLimparCampo = new Command(LimparCampos);
        }
        private void LimparCampos()
        {
            CEP = string.Empty;
            EstaAtivo = false;
        }
        private async Task LocalizaEnderecoCEPAsync()
        {
            var conferirAcessoInternet = Connectivity.NetworkAccess;
            if(conferirAcessoInternet == NetworkAccess.Internet)
            {
                
                if (string.IsNullOrWhiteSpace(CEP))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "O CEP não poder fica vazio ou conter espaços", "Fechar");
                    return;
                }
                if(CEP.Length < 8)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "O CEP deve conter obrigatoriamente 8 números", "Fechar");
                    return;
                }
                if (CEP.Contains("-"))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "O CEP deve conter apenas números", "Fechar");
                    return;

                }
                else
                {
                    var result = await _service.BuscarEnderecoAsync(CEP);
                    if(!string.IsNullOrEmpty(result.Estado))
                    {
                        EstaAtivo = true;
                        Estado = result.Estado;
                        Cidade = result.Cidade;
                        Bairro = result.Bairro == "" ? "Cidade com CEP Universal" : result.Bairro;
                        Logradouro = result.Bairro == "" ? "" : result.Logradouro;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "CEP inválido, verifique e tente novamente", "Fechar");
                        return;
                    }

                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Você deve estar conectado à internet", "Fechar");
                return;
            }
          
        }
    }
}
