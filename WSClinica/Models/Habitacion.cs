namespace WSClinica.Models
{
    public class Habitacion
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public int ClinicaID { get; set; } //Clave Foranea

        public Clinica Clinica { get; set;}
    }
}
