using System.ComponentModel.DataAnnotations;

namespace ComandaEletrônica.Entities
{
    public class COMANDA
    {
        public COMANDA(DateTime datadacompra, double valortotal)
        {
            Datadacompra = datadacompra;
            Valortotal = valortotal;
        }

        public DateTime Datadacompra { get; set; }
        [Key]
        public int Idcomanda { get; set; }
        public double Valortotal { get; set; }
    }
}