﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeXML
{
    class Produto
    {
        public string id { get; set; }
        public string nome { get; set; }
        public double preco { get; set; }
        public string idCategoria { get; set; }
        public int qtdEstoque { get; set; }
        public int qtdMinEstoque { get; set; }

        public Produto(string nome, string idCategoria, double preco, int qtdEstoque, int qtdMinEstoque)
        {
            id = Guid.NewGuid().ToString().Substring(9, 4).ToUpper();
            this.nome = nome;
            this.preco = preco;
            this.idCategoria = idCategoria;
            this.qtdEstoque = qtdEstoque;
            this.qtdMinEstoque = qtdMinEstoque;
        }

        public Produto(string id, string idCategoria, string nome, double preco, int qtdEstoque, int qtdMinEstoque)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.idCategoria = idCategoria;
            this.qtdEstoque = qtdEstoque;
            this.qtdMinEstoque = qtdMinEstoque;
        }
    }
}
