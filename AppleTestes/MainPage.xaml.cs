using AppleTestes.Interfaces;
using AppleTestes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppleTestes
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
           
            InitializeComponent();
           
        }
        protected override async void OnAppearing()
        {
            await DependencyService.Get<IServiceLocalizaEndereco<Endereco>>().BuscarEnderecoAsync("77600000");
            base.OnAppearing();
        }


    }
}
