using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace testeXML
{
    class Estoque
    {
        XElement produtos, categorias;
        string prodPath, catPath;

        public Estoque()
        {
            carregaArquivos();
        }

        public void carregaArquivos()
        {
            prodPath = "../../produtos.xml";
            catPath = "../../categorias.xml";
            produtos = XElement.Load(prodPath);
            categorias = XElement.Load(catPath);
        }

        public void inserirProduto(Produto produto)
        {
            XElement p = new XElement("Produto");
            p.Add(new XAttribute("id", produto.id));
            p.Add(new XAttribute("idCategoria", produto.idCategoria));
            p.Add(new XAttribute("nome", produto.nome));
            p.Add(new XAttribute("preco", produto.preco.ToString()));
            p.Add(new XAttribute("qtdEstoque", produto.qtdEstoque.ToString()));
            p.Add(new XAttribute("qtdMinEstoque", produto.qtdMinEstoque.ToString()));
            produtos.Add(p);
            produtos.Save(prodPath);
        }

        public void editarProduto(string idProduto, Produto novoProduto)
        {
            XElement prod = produtos.Elements().Where(p => p.Attribute("id").Value.Equals(novoProduto.id.ToString())).First();
            if (prod != null)
            {
                prod.Attribute("nome").SetValue(novoProduto.nome);
                prod.Attribute("preco").SetValue(novoProduto.preco);
                prod.Attribute("idCategoria").SetValue(novoProduto.idCategoria);
            }
            produtos.Save(prodPath);
        }

        public List<Produto> consultarProdutos()
        {
            List<Produto> prods = new List<Produto>();
            foreach (XElement item in produtos.Elements())
            {
                Produto p = new Produto(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("idCategoria").Value,
                    (string)item.Attribute("nome").Value,
                    double.Parse(item.Attribute("preco").Value),
                    int.Parse(item.Attribute("qtdEstoque").Value),
                    int.Parse(item.Attribute("qtdMinEstoque").Value)
                    );
                prods.Add(p);
            }
            return prods;
        }

        public void deletarProduto(string idProduto)
        {
            XElement prod = produtos.Elements().Where(p => p.Attribute("id").Value.Equals(idProduto)).First();
            if (prod != null)
            {
                prod.Remove();
            }
            produtos.Save(prodPath);
        }

        public List<Produto> consultarProdutoPorCategoria(string idCategoria)
        {
            List<Produto> prods = new List<Produto>();
            foreach (XElement item in produtos.Elements().Where(p => p.Attribute("id").Value.Equals(idCategoria)))
            {
                Produto p = new Produto(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("idCategoria").Value,
                    (string)item.Attribute("nome").Value,
                    double.Parse(item.Attribute("preco").Value),
                    int.Parse(item.Attribute("qtdEstoque").Value),
                    int.Parse(item.Attribute("qtdMinEstoque").Value)
                    );
                prods.Add(p);
            }
            return prods;
        }

        //--------------------------------------------------------------------------
        //------------------------ C A T E G O R I A S -----------------------------
        //--------------------------------------------------------------------------

        public void inserirCategoria(Categoria categoria)
        {
            XElement p = new XElement("Categoria");
            p.Add(new XAttribute("id", categoria.id));
            p.Add(new XAttribute("nome", categoria.nome));
            p.Add(new XAttribute("descricao", categoria.descricao));
            categorias.Add(p);
            categorias.Save(catPath);
        }

        public void editarcategoria(string idCategoria, Categoria novaCategoria)
        { 
            XElement cat = categorias.Elements().Where(c => c.Attribute("id").Value.Equals(novaCategoria.id)).First();
            if (cat != null)
                cat.Attribute("descricao").SetValue(novaCategoria.descricao);

            categorias.Save(catPath);
        }

        public List<Categoria> consultarCategorias()
        {
            List<Categoria> catList = new List<Categoria>();
            foreach (XElement item in categorias.Elements())
            {
                Categoria c = new Categoria(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("nome").Value,
                    (string)item.Attribute("descricao").Value
                    );
                catList.Add(c);
            }
            return catList;
        }

        public void deletarCategoria(string idCategoria)
        {
            XElement cat = categorias.Elements().Where(c => c.Attribute("id").Value.Equals(idCategoria)).First();
            if (cat != null)
                cat.Remove();
            categorias.Save(catPath);
        }
    }
}
