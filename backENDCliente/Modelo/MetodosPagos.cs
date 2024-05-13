using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backENDCliente.Modelo
{
    public class MetodosPagos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMetodoPago { get; set; }
        public int idCliente { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string nombreTitular { get; set; }
    }
}
