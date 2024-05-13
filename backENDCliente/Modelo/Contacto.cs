using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backENDCliente.Modelo
{
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContacto { get; set; }
        public int idCliente { get; set; }
        public string TipoContacto { get; set; }
        public string ValorContacto { get; set; }
    }
}
