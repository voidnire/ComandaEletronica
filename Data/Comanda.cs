namespace ComandaEletrônica.Data
{
    public class Comanda
    {
        public DateTime DatadaCompra { get; set; }

        public int Id { get; set; }

        public Produto produto { get; set; }
    }
}