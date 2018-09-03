using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using H4Visita.Data;
using H4Visita.Models;
using System.Collections.ObjectModel;
using SQLite;
using System.IO;

namespace H4Visita
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Empresa> obsEmpresas = new ObservableCollection<Empresa>();
        //Database db = new Database("https://h4visita-1533134851816.firebaseio.com/");
        SQLiteAsyncConnection db;
        IRepository<Empresa> repEmpresa;
        IRepository<Visita> repVisitas;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            obsEmpresas.Clear();
            BDLocal_Load();
            LoadEmpresas();
        }

        public MainPage()
        {
            InitializeComponent();
            //LoadEmpresas();            

            this.BindingContext = obsEmpresas;
            this._lstEmpresas.BindingContext = obsEmpresas;
            this._lstEmpresas.ItemTapped += _lstEmpresas_OnTapped;           
        }

        private async void BDLocal_Load() {            
            try
            {
                db = DependencyService.Get<IDatabaseConnection>().DbConnection();
                await db.CreateTablesAsync<Empresa, Visita>();
                repEmpresa = new Repository<Empresa>(db);
                repVisitas = new Repository<Visita>(db);
            }
            catch (SQLiteException ex) {
                await DisplayAlert("Erro", ex.Message, "OK");
            }            
        }

        private async void bntAddEmpresa_OnClicked(object sender, EventArgs args)
        {
           // await Navigation.PushAsync(new Views.Empresa(-1, db, obs));
        }

        private async void _lstEmpresas_OnTapped(object sender, ItemTappedEventArgs e)
        {
            //await Navigation.PushAsync(new Views.Empresa(((Empresa)e.Item).id,db, obs));
        }

        private async void LoadEmpresas() {

            var dblit = await repEmpresa.Get();

            foreach (var item in dblit)
            {
                if (!obsEmpresas.Contains(item))
                {
                    obsEmpresas.Add(item);                    
                }
            }
        }

        private void srcEmpresa_Search(object sender, EventArgs e)
        {
            var keywords = srcEmpresa.Text;

            this._lstEmpresas.ItemsSource = obsEmpresas.Where(i=>
                i.razao.ToLower().Contains(keywords.ToLower()));
        }
    }
}
