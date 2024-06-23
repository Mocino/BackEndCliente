namespace backENDCliente.DTO
{
    public class ClienteGuardar
    {
        public class CreateClientRequest
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Direccion { get; set; }
            public DateTime FechaNacimiento { get; set; }
            public string DPI { get; set; }
            public string NIT { get; set; }
            public string Empresa { get; set; }
            public List<ContactoDto> Contactos { get; set; }
            public List<MetodoPagoDto> MetodosPagos { get; set; }
        }

        public class ContactoDto
        {
            public string TipoContacto { get; set; }
            public string ValorContacto { get; set; }
        }

        public class MetodoPagoDto
        {
            public string Tipo { get; set; }
            public string Numero { get; set; }
            public DateTime FechaVencimiento { get; set; }
            public string NombreTitular { get; set; }
        }

    }
}
