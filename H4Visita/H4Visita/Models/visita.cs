using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;


namespace H4Visita.Models
{
    [Table("Visitas")]
    public class Visita
    {
        public Visita() {

        }

        public Visita(string _dtVisita, string _obs, int _empresa_id) {
            this.dtVisita = _dtVisita;
            this.observacao = _obs;
            this.empresa_id = _empresa_id;
        }

        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [JsonProperty("dtvisita")]
        public string dtVisita { get; set; }
        [JsonProperty("observacao")]
        public string observacao { get; set; }
        [JsonProperty("empresa_id")]
        public int empresa_id { get; set; }
    }
}
