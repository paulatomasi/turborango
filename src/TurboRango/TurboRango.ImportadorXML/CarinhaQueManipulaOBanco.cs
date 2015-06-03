using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class CarinhaQueManipulaOBanco
    {
        private string connectionString;

        public CarinhaQueManipulaOBanco(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void Inserir(Contato contato)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES  (@Site, @Telefone)";
                using (var inserirContato = new SqlCommand(comandoSQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;

                    connection.Open();
                    inserirContato.ExecuteNonQuery();
                }
            }
        }

        internal IEnumerable<Contato> GetContatos()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                string comandoSQL = "SELECT [Site], [Telefone] FROM [dbo].[Contato]";
                using (var lerContato = new SqlCommand(comandoSQL, connection))
                {
                    connection.Open();
                    var reader = lerContato.ExecuteReader();

                    while (reader.Read())
                    {
                        // indice do retorno das colunas
                        string site = reader.GetString(0);
                        string telefone = reader.GetString(1);
                    }
                }
            }
            throw new NotImplementedException();
        }
    }
}