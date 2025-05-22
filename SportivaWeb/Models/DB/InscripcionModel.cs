namespace SportivaWeb.Models.DB
{
    public class InscripcionModel
    {
        public int Id { get; set; }
        public int Usuario { get; set; }
        public int Evento { get; set; }
        public DateTime Fecha { get; set; } = DateTime.MinValue;
    }
}
