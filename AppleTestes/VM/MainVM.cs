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
            set
            {
                _estaAtivo = value;
                OnPropertyChange(nameof(EstaAtivo));
            }
        }
        private bool _enableCampos;

        public bool EnableCampos
        {
            get { return _enableCampos; }
            set
            {
                _enableCampos = value;
                OnPropertyChange(nameof(EnableCampos));
            }
        }
        private bool _enableActivity;

        public bool EnableActivity
        {
            get { return _enableActivity; }
            set { _enableActivity = value; OnPropertyChange(nameof(EnableActivity)); }
        }


        private IServiceLocalizaEndereco<Endereco> _service;
        private bool _absOnOff;

        public bool AbsOnOff
        {
            get { return _absOnOff; }
            set { _absOnOff = value;
                OnPropertyChange(nameof(AbsOnOff));
            }
        }




        public MainVM()
        {
            _service = new ServiceLocalizaEndereco();
            EnableActivity = false;

            EnableCampos = true;
            EstaAtivo = false;
            BuscarEndereco = new Command(async () => await LocalizaEnderecoCEPAsync());

            BtnLimparCampo = new Command(LimparCampos);
        }
        private void LimparCampos()
        {
           

            CEP = string.Empty;
            EstaAtivo = false;
            EnableActivity = false;
        }
        private void LimparCampo2()
        {
            EnableCampos = false;
            EstaAtivo = false;
            EnableActivity = false;
            EnableCampos = true;
        }
        private async Task LocalizaEnderecoCEPAsync()
        {
           
            var conferirAcessoInternet = Connectivity.NetworkAccess;
            if (conferirAcessoInternet == NetworkAccess.Internet)
            {
                

                if (string.IsNullOrWhiteSpace(CEP))
                {
                    await MessageUsuario("Error", "O CEP não poder fica vazio ou conter espaços");
                    return;
                }
                else if (CEP.Length < 8)
                {
                    await MessageUsuario("Error", "O CEP deve conter obrigatoriamente 8 números");
                    return;
                }
                else if (CEP.Contains(".") || CEP.Contains("-"))
                {
                    await MessageUsuario("Info", "CEP deve conter apenas números");
                    return;
                }
                else
                {
                    var result = await _service.BuscarEnderecoAsync(CEP);
                    switch (result.Estado)
                    {
                        case null:
                            await MessageUsuario("Info", "CEP inválido, verifique e tente novamente");
                            break;
                        default:
                            AbsOnOff = true;
                            EnableCampos = false;
                            EnableActivity = true;
                            // EstaAtivo = true;


                            await Task.Delay(1000).ContinueWith(a =>
                            {
                                EstaAtivo = true;
                                Estado = result.Estado;
                                Cidade = result.Cidade;
                                Bairro = result.Bairro == "" ? "Cidade com CEP Universal" : result.Bairro;
                                Logradouro = result.Bairro == "" ? "" : result.Logradouro;
                                EnableActivity = false;
                                EnableCampos = true;
                                AbsOnOff = false;

                            });
                            break;
                    }
                    /*if (!string.IsNullOrEmpty(result.Estado))
                    {
                        EnableActivity = true;
                        // EstaAtivo = true;


                        await Task.Delay(1000).ContinueWith(a =>
                        {
                            EstaAtivo = true;
                            Estado = result.Estado;
                            Cidade = result.Cidade;
                            Bairro = result.Bairro == "" ? "Cidade com CEP Universal" : result.Bairro;
                            Logradouro = result.Bairro == "" ? "" : result.Logradouro;
                            EnableCampos = true;
                            EnableActivity = false;
                        });
                       
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Info", "CEP inválido, verifique e tente novamente", "Fechar");
                        return;
                    }*/

                }
            }
            else
            {
                await MessageUsuario("Error", "Você deve estar conectado à internet");
                return;
            }

        }
        private static async Task MessageUsuario(string title, string msg, string close = "Fechar")
        {

            await Application.Current.MainPage.DisplayAlert(title, msg, close);

        }
    }
}
