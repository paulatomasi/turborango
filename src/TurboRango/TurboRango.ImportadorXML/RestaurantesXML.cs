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

        /// <summary>
        /// Constrói RestaurantesXML a partir do nome de um arquivo.
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo a ser manipulado.</param>
        public RestaurantesXML(string nomeArquivo)
        {
            this.NomeArquivo = nomeArquivo;
            this.restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
        }

        // 1A
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
                where Convert.ToInt32(n.Attribute("capacidade").Value) < 100
                select n.Attribute("nome").Value
                ).ToList();
        }
        
        // 1B (retornar sites dos restaurantes que tem)
        // public IList<string> ObterSites()

        // 1C
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

        // 1D
        public IList<Restaurante> AgruparPorCategoria()
        {
            var res = from n in restaurantes
                      group n by n.Attribute("categoria").Value into g
                      select new { Categoria = g.Key, 
                                   Restaurantes = g.ToList(), 
                                   SomatorioCapacidades = g.Sum(x => Convert.ToInt32(x.Attribute("capacidade").Value))
                                 };
            throw new NotImplementedException();
        }

        // 1E (retornar categorias que têm apenas um restaurante
        // public IList<Categoria> ApenasComUmRestaurante()

        // 1F (retornar as duas categorias mais populares (+ 2 restaurantes),
        // ordenadas por quantidade de restaurantes descendente)
        // public IList<Categoria> ApenasMaisPopulares()

        // 1G (retornar os 8 bairros com menos pizzarias)
        // public IList<string> BairrosComMenosPizzarias()

        // 1H (retornar grupos anônimos (chave de agrupamento: bairro,
        // valor agrupado: percentual) ordenados pelo percentual de forma
        // descendente. Arredonde o percentual em duas casas de precisão.
        // Percentual: nº de restaurantes no bairro / nº total de 
        // restaurantes na cidade
        // public object AgrupadosPorBairroPercentual()
    }
}