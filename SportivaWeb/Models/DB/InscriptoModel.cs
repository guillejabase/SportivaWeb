namespace SportivaWeb.Models.DB
{
    public class InscriptoModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public int Provincia { get; set; }

        public int CodPostal { get; set; }

        public int DNI { get; set; }

        public DateTime FechaNaci { get; set; }

        public int Sexo { get; set; }

        public string Telefono { get; set; } = string.Empty;
    }
}
