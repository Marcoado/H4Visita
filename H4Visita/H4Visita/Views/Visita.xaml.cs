using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using H4Visita.Models;
using H4Visita.Data;

using SQLite;


namespace H4Visita.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Visita : ContentPage
    {

        ObservableCollection<Models.Visita> ObsVisitas;
        int nId;
        SQLiteAsyncConnection db;

        public Visita(int _nId,SQLiteAsyncConnection _db)
        {
            InitializeComponent();
            nId = _nId;
            this.db = _db;            
            this.ObsVisitas = _Obs;
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            List<Models.Visita> lstVisitas = new List<Models.Visita>();

            var itens = ObsVisitas.Single(i=>i.id == nId);

            //if (itens.Visitas != null) {
            //    lstVisitas = itens.Visitas;
            //}

            lstVisitas.Add(new Models.Visita(dtVisita.Date.ToString(), txtObservacoes.Text,nId));

            //oObs.Single(i=>i.id == nId).Visitas = lstVisitas;

            if (nId >= 0) {
                DisplayAlert("Erro ao add a visita!", "Antes de adicionar a visita é necessário salvar a Empresa!", "OK");
            }
            else {
                //db.UpdateEmpresa(oObs.Single(i=>i.uid == cUid));
            }
            
            await Navigation.PopModalAsync();
        }
    }
}