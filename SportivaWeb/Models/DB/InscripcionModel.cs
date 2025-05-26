namespace SportivaWeb.Models.DB
{
    public class InscripcionModel
    {
        public int Id { get; set; }

        public int Inscripto { get; set; }

        public int Evento { get; set; }

        public DateTime Fecha { get; set; }
    }
}
