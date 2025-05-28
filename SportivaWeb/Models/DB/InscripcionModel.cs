namespace SportivaWeb.Models.DB
{
    public class InscripcionModel
    {
        public int Id { get; set; }

        public int Inscripto { get; set; } // FK InscriptoModel

        public int Evento { get; set; } // FK EventoModel

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
