using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            //caminhos dos arquivos dos produtos XML
            prodPath = "produtos.xml";
            catPath = "categorias.xml";
            carregaArquivos();
        }

        //carrega os arquivos XML que serão usados durante a execução do programa
        public void carregaArquivos()
        {
            try
            {
                produtos = XElement.Load(prodPath);
            }
            catch (System.Exception)
            {
                criaArquivoProdutos();
                produtos = XElement.Load(prodPath);
            }

            try
            {
                categorias = XElement.Load(catPath);
            }
            catch (System.Exception)
            {
                criaArquivoCategorias();
                categorias = XElement.Load(catPath);
            }
        }

        //caso não exista, cria o arquivo XML de produtos
        public void criaArquivoProdutos()
        {
            new XDocument(
                new XElement("Produtos")
            )
        .Save(prodPath);
        }

        //caso não exista, cria o arquivo XML de categorias
        public void criaArquivoCategorias()
        {
            new XDocument(
                new XElement("Categorias")
            )
        .Save(catPath);
        }

        //recupera os valores do list e adiciona ao dataTable
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        //cria um datatable
        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                // HERE IS WHERE THE ERROR IS THROWN FOR NULLABLE TYPES
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        //cria um elemento XML e adiciona os atributos para criar um novo produto
        //deve receber como parametro um objeto do tipo "Produto"
        public void inserirProduto(Produto produto)
        {
            XElement p = new XElement("Produto");
            p.Add(new XAttribute("id", produto.id));
            p.Add(new XAttribute("idCategoria", produto.idCategoria));
            p.Add(new XAttribute("nome", produto.nome));
            p.Add(new XAttribute("descricao", produto.descricao));
            p.Add(new XAttribute("preco", produto.preco.ToString()));
            p.Add(new XAttribute("qtdEstoque", produto.qtdEstoque.ToString()));
            p.Add(new XAttribute("qtdMinEstoque", produto.qtdMinEstoque.ToString()));
            produtos.Add(p);
            produtos.Save(prodPath);
        }

        //carrega um elemento XML de produto de acordo com o "id" e adiciona os atributos para editar o produto existente
        //deve receber como parametro o "id" do produto que será editado e um objeto do tipo "Produto" que irá conter os novos dados
        public void editarProduto(string idProduto, Produto novoProduto)
        {
            XElement prod = produtos.Elements().Where(p => p.Attribute("id").Value.Equals(idProduto)).First();
            if (prod != null)
            {
                prod.Attribute("nome").SetValue(novoProduto.nome);
                prod.Attribute("descricao").SetValue(novoProduto.descricao);
                prod.Attribute("preco").SetValue(novoProduto.preco);
                prod.Attribute("idCategoria").SetValue(novoProduto.idCategoria);
                prod.Attribute("qtdEstoque").SetValue(novoProduto.qtdEstoque);
                prod.Attribute("qtdMinEstoque").SetValue(novoProduto.qtdMinEstoque);
            }
            produtos.Save(prodPath);
        }

        //retorna um "List" com os produtos encontrados
        //se não houver produtos é retornado um "List" vazio
        public DataTable consultarProdutos()
        {
            List<Produto> prods = new List<Produto>();
            foreach (XElement item in produtos.Elements())
            {
                Produto p = new Produto(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("idCategoria").Value,
                    (string)item.Attribute("nome").Value,
                    (string)item.Attribute("descricao").Value,
                    double.Parse(item.Attribute("preco").Value),
                    int.Parse(item.Attribute("qtdEstoque").Value),
                    int.Parse(item.Attribute("qtdMinEstoque").Value)
                    );
                prods.Add(p);
            }
            return ConvertTo<Produto>(prods);
        }

        //exclui do arquivo o produto cujo "id" é fornecido como parametro
        //se o produto não for encontrado ele lança um erro (necessário implementar try-catch para o form)
        public void deletarProduto(string idProduto)
        {
            try
            {
                XElement prod = produtos.Elements().Where(p => p.Attribute("id").Value.Equals(idProduto)).First();
                if (prod != null)
                {
                    prod.Remove();
                }
                produtos.Save(prodPath);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //retorna um "datatable" com os produtos encontrados de acordo com o "id" da categoria fornecido como parametro
        //se não houver produtos é retornado um "datatable" vazio
        public DataTable consultarProdutoPorCategoria(string idCategoria)
        {
            List<Produto> prods = new List<Produto>();
            foreach (XElement item in produtos.Elements().Where(p => p.Attribute("idCategoria").Value.Equals(idCategoria)))
            {
                Produto p = new Produto(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("idCategoria").Value,
                    (string)item.Attribute("nome").Value,
                    (string)item.Attribute("descricao").Value,
                    double.Parse(item.Attribute("preco").Value),
                    int.Parse(item.Attribute("qtdEstoque").Value),
                    int.Parse(item.Attribute("qtdMinEstoque").Value)
                    );
                prods.Add(p);
            }
            return ConvertTo<Produto>(prods);
        }

        //retorna um "datatable" com os produtos encontrados de acordo com a palavra passada por parametro
        //se não houver produtos é retornado um "datatable" vazio
        public DataTable consultarProdutoPorNomeOuDescricao(string subString)
        {
            List<Produto> prods = new List<Produto>();
            foreach (XElement item in produtos.Elements().Where(p => p.Attribute("descricao").Value.Contains(subString) || p.Attribute("nome").Value.Contains(subString)))
            {
                Produto p = new Produto(
                    (string)item.Attribute("id").Value,
                    (string)item.Attribute("idCategoria").Value,
                    (string)item.Attribute("nome").Value,
                    (string)item.Attribute("descricao").Value,
                    double.Parse(item.Attribute("preco").Value),
                    int.Parse(item.Attribute("qtdEstoque").Value),
                    int.Parse(item.Attribute("qtdMinEstoque").Value)
                    );
                prods.Add(p);
            }
            return ConvertTo<Produto>(prods);
        }

        //--------------------------------------------------------------------------
        //------------------------ C A T E G O R I A S -----------------------------
        //--------------------------------------------------------------------------

        //cria um elemento XML e adiciona os atributos para criar uma nova categoria
        //deve receber como parametro um objeto do tipo "Categoria"
        public void inserirCategoria(Categoria categoria)
        {
            XElement p = new XElement("Categoria");
            p.Add(new XAttribute("id", categoria.id));
            p.Add(new XAttribute("nome", categoria.nome));
            p.Add(new XAttribute("descricao", categoria.descricao));
            categorias.Add(p);
            categorias.Save(catPath);
        }

        //carrega um elemento XML de categoria de acordo com o "id" e adiciona os atributos para editar a categoria existente
        //deve receber como parametro o "id" da categoria que será editado e um objeto do tipo "Categoria" que irá conter os novos dados
        public void editarcategoria(string idCategoria, Categoria novaCategoria)
        { 
            XElement cat = categorias.Elements().Where(c => c.Attribute("id").Value.Equals(novaCategoria.id)).First();
            if (cat != null)
                cat.Attribute("descricao").SetValue(novaCategoria.descricao);

            categorias.Save(catPath);
        }

        //retorna um "datatable" com as categorias encontradas
        //se não houver categorias é retornado um "datatable" vazio
        public DataTable consultarCategorias()
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
            return ConvertTo<Categoria>(catList); ;
        }

        //exclui do arquivo a categoria cujo "id" é fornecido como parametro
        //se a categoria não for encontrada ele lança um erro (necessário implementar try-catch para o form)
        public void deletarCategoria(string idCategoria)
        {
            XElement cat = categorias.Elements().Where(c => c.Attribute("id").Value.Equals(idCategoria)).First();
            if (cat != null)
                cat.Remove();
            categorias.Save(catPath);
        }
    }
}
