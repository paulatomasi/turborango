using System;
using System.Collections.Generic;
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
            Console.WriteLine(restaurante.Nome ?? "Nulo!!");
            Console.WriteLine(!string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "Tem valor" : "Não tem valor");
            #endregion

            const string nomeArquivo = "restaurantes.xml";

            var restaurantesXML = new RestaurantesXML(nomeArquivo);

            #region exercício 1
            var obterNomes = restaurantesXML.ObterNomesAsc();
            var obterSites = restaurantesXML.ObterSites();
            var capacidadeMedia = restaurantesXML.CapacidadeMedia();
            var agruparPorCategoria = restaurantesXML.AgruparPorCategoria();
            var apenasUmRestaurante = restaurantesXML.ApenasComUmRestaurante();
            var maisPopulares = restaurantesXML.ApenasMaisPopulares();
            var bairrosComMenosPizzarias = restaurantesXML.BairrosComMenosPizzarias();
            var bairrosPorPercentual = restaurantesXML.AgrupadosPorBairroPercentual();
            #endregion

            #region ADD.NET
            var connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TurboRango_dev;Integrated Security=True;";
            var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);
            acessoAoBanco.Inserir(new Contato
            {
                Site = "www.dogao.gif",
                Telefone = "55555"
            });

            IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();

            #endregion

            var restaurantes = new Restaurantes(connString);
            #region tema ex1
            restaurantes.Inserir(new Restaurante
            {
                Nome = "Tiririca",
                Capacidade = 50,
                Categoria = Categoria.Fastfood,
                Contato = new Contato
                {
                    Site = "http://github.com/tiririca",
                    Telefone = "5555 5555"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Vila Nova",
                    Logradouro = "ERS 239, 2755",
                    Latitude = -29.6646122,
                    Longitude = -51.1188255
                }
            });

            #endregion

            #region tema ex2
            var todosRestaurantes = restaurantesXML.TodosRestaurantes();
            foreach (var restauranteAtual in todosRestaurantes)
            {
                restaurantes.Inserir(restauranteAtual);
            }
            #endregion
        }
    }
}