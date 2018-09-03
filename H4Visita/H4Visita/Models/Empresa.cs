using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Newtonsoft.Json;

namespace H4Visita.Models
{
    [Table("Empresas")]
    public class Empresa: INotifyPropertyChanged
    {
        [JsonProperty("id")]
        [PrimaryKey]
        public int id { get; set; }
        [JsonProperty("razao")]
        public string razao { get; set; }
        [JsonProperty("cnpj")]
        public string cnpj { get; set; }
        [JsonProperty("endereco")]
        public string endereco { get; set; }
        [JsonProperty("bairro")]
        public string bairro { get; set; }
        [JsonProperty("cidade")]
        public string cidade { get; set; }
        [JsonProperty("cep")]
        public string cep { get; set; }
        [JsonProperty("uf")]
        public string uf { get; set; }
        [JsonProperty("telefone")]
        public string telefone { get; set; }
        [JsonProperty("celular")]
        public string celular { get; set; }
        [JsonProperty("contato")]
        public string contato { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
