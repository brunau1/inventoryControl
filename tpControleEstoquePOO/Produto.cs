using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpControleEstoquePOO
{
    class Produto
    {
        public string id { get; set; }
        public string nome { get; set; }
        public double preco { get; set; }
        public string idCategoria { get; set; }
        public int qtdEstoque { get; set; }
        public int qtdMinEstoque { get; set; }

        public Produto(string nome, double preco, string idCategoria, int qtdEstoque, int qtdMinEstoque)
        {
            id = Guid.NewGuid().ToString().Substring(9, 4).ToUpper();
            this.nome = nome;
            this.preco = preco;
            this.idCategoria = idCategoria;
            this.qtdEstoque = qtdEstoque;
            this.qtdMinEstoque = qtdMinEstoque;
        }
    }
}
