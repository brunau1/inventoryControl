using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeXML
{
    class Categoria
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public Categoria(string nome, string descricao)
        {
            id = Guid.NewGuid().ToString().Substring(9, 4).ToUpper();
            this.nome = nome;
            this.descricao = descricao;
        }
        public Categoria(string id, string nome, string descricao)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
        }
    }
}
