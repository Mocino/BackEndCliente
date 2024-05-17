using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backENDCliente.Modelo
{
    public class TypeContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTipoContacto { get; set; }
        public string nombre { get; set; }
        public string valor { get; set; }
        public string Estatus { get; set; } = "A";

    }
}


