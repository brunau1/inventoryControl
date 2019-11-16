using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Estoque estoque = new Estoque();
            Produto p = new Produto("meu carai", "888", 5.5, 3, 1);
            Categoria c = new Categoria("900", "penis", "sua mae e minha");
            estoque.inserirProduto(p);
            estoque.inserirCategoria(c);

            Console.WriteLine("Produtos===================================");
            foreach (Produto prod  in estoque.consultarProdutos())
            {
                Console.WriteLine($"id: {prod.id}");
                Console.WriteLine($"id categoria: {prod.idCategoria}");
                Console.WriteLine($"nome: {prod.nome}");
                Console.WriteLine($"preco: {prod.preco}");
                Console.WriteLine($"estoque: {prod.qtdEstoque}");
                Console.WriteLine($"min estoque: {prod.qtdMinEstoque}");
            }
            Console.WriteLine("==============================================");
            Console.WriteLine("Categorias====================================");
            foreach (Categoria cat in estoque.consultarCategorias())
            {
                Console.WriteLine($"id: {cat.id}");
                Console.WriteLine($"nome: {cat.nome}");
                Console.WriteLine($"descricao: {cat.descricao}");
            }
            Console.WriteLine("==============================================");
            Console.ReadKey();
        }
    }
}
