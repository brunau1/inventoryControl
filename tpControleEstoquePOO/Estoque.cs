using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace tpControleEstoquePOO
{
    class Estoque
    {
        XElement produtos, categorias;

        public Estoque()
        {
            produtos = XElement.Load(@"C:\produtos.xml");
            categorias = XElement.Load(@"C:\categorias.xml");
        }

        /// <summary>
        /// Função que registra um produto no arquivo produtos.xml
        /// </summary>
        /// <param name="produto"></param>
        public void inserirProduto(Produto produto)
        {
            XElement novoProduto = new XElement("Produto",
                                     new XElement("id", produto.nome),
                                     new XElement("idCategoria", produto.nome),
                                     new XElement("nome", produto.nome),
                                     new XElement("preco", produto.nome),
                                     new XElement("qtdEstoque", produto.nome),
                                     new XElement("qtdMinEstoque", produto.nome)
                                     );
            produtos.Add(novoProduto);
            produtos.Save(@"C:\produtos.xml");
        }
        /// <summary>
        /// Recebe um id e um produto com as novas informações para alterar o registro existente
        /// </summary>
        /// <param name="idProduto"></param>
        /// <param name="novoProduto"></param>
        public void editarProduto(string idProduto, Produto novoProduto)
        {
            var produtoTemp = from item in produtos.Elements("Produto")
                              where ((string)item.Element("id")).Equals(idProduto)
                              select item;
            foreach (XElement element in produtoTemp)
            {
                element.SetElementValue("nome", novoProduto.nome);
                element.SetElementValue("preco", novoProduto.preco);
            }
            produtos.Save(@"C:\produtos.xml");
        }

        /// <summary>
        /// Retorna o resultado da consulta dos produtos registrados
        /// </summary>
        /// <returns></returns>
        public object consultarProdutos()
        {
            var consulta = from produto in produtos.Elements("Produto")
                           orderby (string)produto.Element("Nome")
                           select new
                           {
                               id = (string)produto.Element("id"),
                               nome = (string)produto.Element("nome"),
                               preco = (double)produto.Element("preco"),
                               qtdEstoque = (int)produto.Element("qtdEstoque"),
                           };
            return consulta;
        }

        /// <summary>
        /// remove um produto registrado de acordo com o ID do produto
        /// </summary>
        /// <param name="idProduto"></param>
        public void deletarProduto(string idProduto)
        {
            IEnumerable<XElement> produtoTemp = from item in produtos.Elements("Produto")
                                                 where ((string)item.Element("id")).Equals(idProduto)
                                                 select item;

            foreach (XElement element in produtoTemp)
                element.Element("id").Parent.Remove();
            produtos.Save(@"C:\produtos.xml");
        }

        /// <summary>
        /// Consulta produtos pertencentes a uma determinada categoria
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        public object consultarProdutoPorCategoria(string idCategoria)
        {
            var consulta = from produto in produtos.Elements("Produto")
                           where ((string)produto.Element("idCategoria")).Equals(idCategoria)
                           orderby (string)produto.Element("Nome")
                           select new
                           {
                               id = (string)produto.Element("id"),
                               nome = (string)produto.Element("nome"),
                               preco = (double)produto.Element("preco"),
                               qtdEstoque = (int)produto.Element("qtdEstoque"),
                           };
            return consulta;
        }

        public void inserirCategoria(Categoria categoria)
        {
            XElement novaCategoria = new XElement("Categoria",
                                     new XElement("id", categoria.id),
                                     new XElement("nome", categoria.nome),
                                     new XElement("descricao", categoria.descricao)
                                     );
            produtos.Add(novaCategoria);
            produtos.Save(@"C:\categorias.xml");
        }

        public void editarcategoria(string idCategoria, Categoria novaCategoria)
        {
            var categoriaTemp = from item in produtos.Elements("Categoria")
                              where ((string)item.Element("id")).Equals(idCategoria)
                              select item;
            foreach (XElement element in categoriaTemp)
                element.SetElementValue("descricao", novaCategoria.descricao);
            
            produtos.Save(@"C:\categorias.xml");
        }

        public object consultarCategorias()
        {
            var consulta = from categoria in produtos.Elements("Categoria")
                           orderby (string)categoria.Element("nome")
                           select new
                           {
                               id = (string)categoria.Element("id"),
                               nome = (string)categoria.Element("nome"),
                               descricao = (string)categoria.Element("descricao")
                           };
            return consulta;
        }

        public void deletarCategoria(string idCategoria)
        {
            IEnumerable<XElement> produtoTemp = from item in produtos.Elements("Categoria")
                                                where ((string)item.Element("id")).Equals(idCategoria)
                                                select item;

            foreach (XElement element in produtoTemp)
                element.Element("id").Parent.Remove();
            produtos.Save(@"C:\categorias.xml");
        }
    }
}
