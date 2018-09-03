using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using H4Visita.Data;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;

namespace H4Visita.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empresa : TabbedPage
    {
        Geocoder geocoder;
        IGeolocator locator;
        Plugin.Geolocator.Abstractions.Position position;
        IRepository<Models.Empresa> repEmpresas;
        IRepository<Models.Visita> repVisitas;
        int nId;
        public Models.Empresa oEmpresa { get; set; }
        public List<Models.Visita> oVisitas { get; set; }
        public ObservableCollection<Models.Empresa> obsEmpresas { get; set; }
        public ObservableCollection<Models.Visita> obsVisitas{ get; set; }

        public Empresa(int _nId, SQLiteAsyncConnection db)
        {
            _Empresa(_nId, db);
        }

        public async void _Empresa(int _nId, SQLiteAsyncConnection db)
        {

            try
            {
                nId = _nId;                

                this.repEmpresas = new Repository<Models.Empresa>(db);
                this.repVisitas = new Repository<Models.Visita>(db);

                if (nId >= 0)
                {
                    this.oEmpresa = await repEmpresas.Get(nId);
                    obsEmpresas.Add(oEmpresa);
                    this.BindingContext = obsEmpresas;
                }
                else
                {
                    this.oEmpresa = new Models.Empresa();
                    obsEmpresas.Add(oEmpresa);
                    this.BindingContext = obsEmpresas;
                    bindLocation();
                }

                InitializeComponent();
                this._lstVisita.ItemTapped += _lstVisita_OnTapped;
                bntAddVisita.Clicked += bntAddVisita_OnClick;                            
            }
            catch (Exception ex) {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (nId >= 0)
            {
                loadVisitas();
            }
        }

        private void loadVisitas() {            
            if (obsEmpresas.Single(i => i.id == nId) != null)
            {
                this._lstVisita.BeginRefresh();
                this._lstVisita.BindingContext = null;                
                this._lstVisita.BindingContext = (obsVisitas.Select(i => i.empresa_id == nId));
                this._lstVisita.EndRefresh();
            }
        }

        private async void bntAddVisita_OnClick(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Views.Visita(obsVisitas.Single(i => i.empresa_id == nId).empresa_id, db));
        }

       
        private async void _lstVisita_OnTapped(object sender, ItemTappedEventArgs e)
        {
            Models.Visita visita = (Models.Visita)e.Item;
            await DisplayAlert("Observação", visita.observacao, "OK");
        }

        private async void BtnSalvar_OnClick(object sender, EventArgs e) {
            if (this.oEmpresa.id >= 0)
            {                
                await repEmpresas.Insert(this.oEmpresa);
            }
            else {
                await repEmpresas.Update(this.oEmpresa);
            }                       

            await Navigation.PopAsync();
        }

        private async void LoadEmpresas()
        {

            var dblit = await repEmpresas.Get();

            foreach (var item in dblit)
            {
                if (!obsEmpresas.Contains(item))
                {
                    obsEmpresas.Add(item);
                }
            }
        }

        public async void bindLocation()
        {
            try
            {

                this.geocoder = new Geocoder();

                this.locator = CrossGeolocator.Current;

                locator.DesiredAccuracy = 50;

                position = await locator.GetPositionAsync(new TimeSpan(10000));

                Xamarin.Forms.Maps.Position oPosition = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);

                IEnumerable<string> cAddress = await geocoder.GetAddressesForPositionAsync(oPosition);

                string Address = cAddress.First();
                var aAddress = Address.Split('\n');
                string cEndereco = aAddress[0].Split(',')[0];
                string cNumero = aAddress[0].Split(',')[1].Split('-')[0];
                string cBairro = aAddress[0].Split(',')[1].Split('-')[1];

                this.oEmpresa.endereco = cEndereco + ", " + cNumero;
                this.oEmpresa.bairro = cBairro;

                this.endereco.Text = oEmpresa.endereco;
                this.bairro.Text = oEmpresa.bairro;

            }
            catch (Exception ex)
            {
                //Android.Util.Log.Error("AppSA error", ex.Message);
                position = new Plugin.Geolocator.Abstractions.Position();
                position.Latitude = -22.2046169;
                position.Longitude = -49.9743474;
            }
        }
    }
}