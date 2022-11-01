using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ComandaEletrônica.Entities
{
    public class ItemsComanda
    {

        public int Idcomanda { get; set; }
        public DateTime Datacompra { get; set; }
        public IEnumerable<PRODUTO> Items = new List<PRODUTO>(); //podia ser so list msm
        //   {
        //    var listaprodutos = new List<PRODUTO>();

        //    var produto1 = new PRODUTO("ASP, ADO e Banco de dados na web", 3, 22.3);
        //       listaprodutos.Add( produto1);
        //    var produto2 = new PRODUTO("ASP,  na web", 33, 12.32);
        //    listaprodutos.Add(produto2 );
        //        return listaprodutos;
        //    }
    }
}