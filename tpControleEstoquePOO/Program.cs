using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace testeXML
{
    class Program
    {
        static void listaProdutos(List<Produto> prods)
        {
            Console.WriteLine("Produtos===================================");
            foreach (Produto prod in prods)
            {
                Console.WriteLine($"id: {prod.id}");
                Console.WriteLine($"id categoria: {prod.idCategoria}");
                Console.WriteLine($"nome: {prod.nome}");
                Console.WriteLine($"preco: {prod.preco}");
                Console.WriteLine($"estoque: {prod.qtdEstoque}");
                Console.WriteLine($"min estoque: {prod.qtdMinEstoque}");
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.ReadKey();
        }
        static void listaCategorias(List<Categoria> cats)
        {
            Console.WriteLine("Categorias====================================");
            foreach (Categoria cat in cats)
            {
                Console.WriteLine($"id: {cat.id}");
                Console.WriteLine($"nome: {cat.nome}");
                Console.WriteLine($"descricao: {cat.descricao}");
                Console.WriteLine("----------------------------------------");
            }
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Estoque estoque = new Estoque();
            Produto p = new Produto("teste 1", "888", 5.5, 3, 1);
            Produto e = new Produto("teste 2", "456", 5.5, 3, 1);
            Produto o = new Produto("teste com cat 1", "900", 5.5, 3, 1);
            Produto q = new Produto("teste com cat 2", "900", 5.5, 3, 1);
            Categoria c = new Categoria("900", "cat teste 1", "nothing");
            Categoria d = new Categoria("678", "cat teste 2", "nothing");

            //teste de inserção (cadastro) de produtos
            /*estoque.inserirProduto(p);
            estoque.inserirProduto(e);
            estoque.inserirProduto(o);
            estoque.inserirProduto(q);*/

            //teste de edição de produto
            /*estoque.inserirProduto(e);
            listaProdutos(estoque.consultarProdutos());
            estoque.editarProduto(e.id, q);
            listaProdutos(estoque.consultarProdutos());*/

            //teste de insercao de categoria
            /*estoque.inserirCategoria(c);
            estoque.inserirCategoria(d);*/

            //teste de listagem de produtos
            //listaProdutos(estoque.consultarProdutos());

            //teste de listagem das categorias
            //listaCategorias(estoque.consultarCategorias());

            //teste de consulta dos produtos pela categoria (id da categoria como parametro da funcao)
            //listaProdutos(estoque.consultarProdutoPorCategoria("900"));
        }
    }
}
