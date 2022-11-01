using System.ComponentModel.DataAnnotations;

namespace ComandaAPI.Entities
{
    public class COMANDA_PRODUTO
    {
        [Key]
        public int Idcopr { get; set; }
        public int Idcomanda { get; set; }
        public int Idproduto { get; set; }
        public DateTime Datacompra { get; set; }

    }
}
