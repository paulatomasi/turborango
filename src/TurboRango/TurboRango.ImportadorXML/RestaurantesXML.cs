using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public string NomeArquivo { get; private set; }
        IEnumerable<XElement> restaurantes;

        // Construtor
        public RestaurantesXML(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;
            this.restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
        }

        #region exercício 1
        // 1A Obter os nomes dos restaurantes
        public IList<string> ObterNomesAsc()
        {
            #region versão não hacker
            /*var resultado = new List<string>();
            var nodos = XDocument.Load(NomeArquivo).Descendants("restaurante");

            foreach (var item in nodos)
            {
                resultado.Add(item.Attribute("nome").Value);
            }
            return resultado;*/
            #endregion

            return (
                from n in restaurantes
                orderby n.Attribute("nome").Value
                select n.Attribute("nome").Value
                ).ToList();
        }

        // 1B Obter os sites dos restaurantes
        public IList<string> ObterSites()
        {
            return (
                from n in restaurantes
                let contato = n.Element("contato")
                let site = contato != null ? contato.Element("site") : null
                where site != null && !String.IsNullOrEmpty(site.Value)
                select contato.Element("site").Value
                ).ToList();
        }

        // 1C Capacidade média dos restaurantes
        public double CapacidadeMedia()
        {
            return (from n in restaurantes
                    select Convert.ToInt32(n.Attribute("capacidade").Value)
                   ).Average();
        }

        public double CapacidadeMaxima()
        {
            var mad = (from n in restaurantes
                       select Convert.ToInt32(n.Attribute("capacidade").Value)
                       );

            return mad.Max();
        }

        // 1D Agrupa os restauranes por categoria
        public object AgruparPorCategoria()
        {
            return (
                from n in restaurantes
                group n by n.Attribute("categoria").Value into g
                select new { Categoria = g.Key, Restaurantes = g.ToList() }
            ).ToList();
        }

        // 1E Lista as categorias com apenas um restaurante
        public IList<Categoria> ApenasComUmRestaurante() 
        {
            return (
                from n in restaurantes
                let cat = n.Attribute("categoria").Value
                group n by cat into g
                where g.Count() == 1
                select (Categoria)Enum.Parse(typeof(Categoria), g.Key, ignoreCase: true)
                ).ToList();
        }

        // 1F Lista as duas categorias com mais restaurantes
        public IList<Categoria> ApenasMaisPopulares()
        {
            return (
                from n in restaurantes
                group n by n.Attribute("categoria").Value into g
                let groupLength = g.Count()
                where groupLength > 2
                orderby groupLength descending
                select (Categoria)Enum.Parse(typeof(Categoria), g.Key, ignoreCase: true)
            ).Take(2).ToList();
        }

        // 1G Lista os bairros com menos pizzarias
        public IList<string> BairrosComMenosPizzarias()
        {
            return (
                from n in restaurantes
                let cat = (Categoria)Enum.Parse(typeof(Categoria), n.Attribute("categoria").Value, ignoreCase: true)
                where cat == Categoria.Pizzaria
                group n by n.Element("localizacao").Element("bairro").Value into g
                orderby g.Count()
                select g.Key
            ).Take(8).ToList();
        }

        // 1H Agrupa os bairros por percentual de restaurante
        public object AgrupadosPorBairroPercentual()
        {
            return (
                from n in restaurantes
                group n by n.Element("localizacao").Element("bairro").Value into g
                let totalRestaurantes = restaurantes.Count()
                select new { Bairro = g.Key, Percentual = Math.Round(Convert.ToDouble(g.Count() * 100) / totalRestaurantes, 2) }
            ).OrderByDescending(g => g.Percentual);
        }
        #endregion
    }
}