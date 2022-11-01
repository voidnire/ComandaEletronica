using System.ComponentModel.DataAnnotations;

namespace ComandaEletrônica.Entities
{
    public class PRODUTO
    {
        public PRODUTO(string nome, int quantidade, double preco)
        {
            Nome = nome;
            Quantidade = quantidade;
            Preco = preco;
        }


        [Key]
        public int Idproduto { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
    }
}