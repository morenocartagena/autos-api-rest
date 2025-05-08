using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace autos_api_rest.Models
{
    [Table("MarcasAutos")]
    public class MarcaAuto
    {
        [Key]
        [Column("id")]
        [JsonPropertyOrder(1)]
        public int Id { get; set; }

        [Column("marca")]
        [JsonPropertyOrder(2)]
        public string Marca { get; set; }
    }
}
