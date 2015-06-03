using System;
using System.Text;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region pedreiro
            var oQueEuGoasto = "bacon";

            var texto = String.Format("Eu gosto de {0}", oQueEuGoasto);
            Console.WriteLine(texto);

            StringBuilder pedreiro = new StringBuilder();
            pedreiro.AppendFormat("Eu gosto de {0}", oQueEuGoasto);
            pedreiro.AppendFormat("!!!!!!");
            Console.WriteLine(pedreiro);
            #endregion

            #region exemplos
            Restaurante restaurante = new Restaurante();
            //restaurante.Nome = string.Empty;
            restaurante.Nome = "";
            // Se o restaurante tiver capacidade, exibe a capacidade.
            // Se a capacidade for nula, exibe "oi".
            Console.WriteLine(restaurante.Capacidade.HasValue ? restaurante.Capacidade.Value.ToString() : "oi");
            // Operador de coerção nula
            Console.WriteLine(restaurante.Nome ??  "Nulo!!");
            Console.WriteLine(! string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "Tem valor" : "Não tem valor");
            #endregion

            const string nomeArquivo = "restaurantes.xml";

            var restaurantesXML = new RestaurantesXML(nomeArquivo);
            var nomes = restaurantesXML.ObterNomesAsc();
            var capacidadeMedia = restaurantesXML.CapacidadeMedia();
            var capacidadeMaxima = restaurantesXML.CapacidadeMaxima();
            var porCategoria = restaurantesXML.AgruparPorCategoria();

            Console.ReadLine();
        }
    }
}
