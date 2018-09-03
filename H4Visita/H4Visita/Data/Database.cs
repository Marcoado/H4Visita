using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Firebase.Xamarin.Database.Offline;
using H4Visita.Models;

namespace H4Visita.Data
{   
    public class Database
    {
        public FirebaseClient bdFire { get; set; }       

        public Database()
        {
            this.bdFire = new FirebaseClient(this.dbUrl);
        }
        public Database(string dbString) {
            this.dbUrl = dbString;
            this.bdFire = new FirebaseClient(this.dbUrl);
        }

        public async Task<List<Empresa>> GetEmpresas() {

            var list = (await this.bdFire.Child("empresas")
                .OnceAsync<Empresa>())
                .Select(item =>
                    new Empresa {
                        id = item.id,
                        cnpj = item.Object.cnpj,
                        razao = item.Object.razao,
                        endereco = item.Object.endereco,
                        bairro = item.Object.bairro,
                        cidade = item.Object.cidade,
                        cep = item.Object.cep,
                        uf = item.Object.uf,
                        telefone = item.Object.telefone,
                        contato = item.Object.contato
                    }
                ).ToList();
            return list;
        }
       
        public async void InsertEmpresa(Empresa oEmpresa) {
            //EmpresaPost oEmpPost = new EmpresaPost(oEmpresa);
            //var item = (await this.bdFire.Child("empresas")
            //    .PostAsync(oEmpPost));
        }

        public async void UpdateEmpresa(Empresa oEmpresa) {
            //EmpresaPost oEmpPost = new EmpresaPost(oEmpresa);
            //await this.bdFire
            //       .Child("empresas")
            //       .Child(oEmpresa.id)
            //       .PutAsync(oEmpPost);
        }

        public string dbUrl { get; set; }
    }
}
