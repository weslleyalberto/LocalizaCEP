using AppleTestes.Interfaces;
using AppleTestes.Models;
using AppleTestes.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppleTestes
{
    public partial class App : Application
    {
        IServiceLocalizaEndereco<Endereco> _service;
       
        public App()
        {
            DependencyService.Register<IServiceLocalizaEndereco<Endereco>, ServiceLocalizaEndereco>();
           
            
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
          
            _service = new ServiceLocalizaEndereco();
          await  _service.BuscarEnderecoAsync("77600000");
           
            

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
