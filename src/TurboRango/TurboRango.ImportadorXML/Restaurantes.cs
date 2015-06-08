using System;
using System.Data;
using System.Data.SqlClient;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        private string ConnectionString { get; set; }

        public Restaurantes(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        #region Inserir
        internal void Inserir(Restaurante restaurante)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Restaurante] ([Nome], [Capacidade], [Categoria], [ContatoId], [LocalizacaoId]) VALUES (@Nome, @Capacidade, @Categoria, @ContatoId, @LocalizacaoId)";
                using (var inserirRestaurante = new SqlCommand(comandoSQL, connection))
                {
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.NVarChar).Value = restaurante.Capacidade;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.NVarChar).Value = InserirContato(restaurante.Contato); ;
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.NVarChar).Value = InserirLocalizacao(restaurante.Localizacao);
                    connection.Open();
                    inserirRestaurante.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region InserirContato
        private int InserirContato(Contato contato)
        {
            int idCriado;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Contato] ([Site], [Telefone]) VALUES (@Site, @Telefone)";
                string id = "SELECT @@IDENTITY";
                using (var inserirContato = new SqlCommand(comandoSQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site ?? (object)DBNull.Value;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone ?? (object)DBNull.Value;

                    connection.Open();
                    inserirContato.ExecuteNonQuery();
                    inserirContato.CommandText = id;
                    idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());
                }
            }
            return idCriado;
        }
        #endregion

        #region InserirLocalizacao
        private int InserirLocalizacao(Localizacao localizacao)
        {
            int idCriado;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Localizacao] ([Bairro], [Logradouro], [Latitude], [Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude)";
                string id = "SELECT @@IDENTITY";
                using (var inserirLocalizacao = new SqlCommand(comandoSQL, connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    inserirLocalizacao.ExecuteNonQuery();
                    inserirLocalizacao.CommandText = id;
                    idCriado = Convert.ToInt32(inserirLocalizacao.ExecuteScalar());
                }
            }
            return idCriado;
        }
        #endregion

        #region Remover
        private int buscarIdContato(int id){
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                string selecionarContatoId = "SELECT Id FROM Contato WHERE EXISTS (SELECT ContatoId FROM Restaurante WHERE Restaurante.Id = @Id AND Restaurante.ContatoId = Contato.Id)";
                using (var contatoId = new SqlCommand(selecionarContatoId, connection))
                {
                    contatoId.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    int idContato = Convert.ToInt32(contatoId.ExecuteScalar());
                    return idContato;
                }
            }
        }

        private int buscarIdLocalizacao(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                string selecionarLocalicazaoId = "SELECT Id FROM Localizacao WHERE EXISTS (SELECT LocalizacaoId FROM Restaurante WHERE Restaurante.Id = @Id AND Restaurante.LocalizacaoId = Localizacao.Id)";
                using (var localizacaoId = new SqlCommand(selecionarLocalicazaoId, connection))
                {
                    localizacaoId.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    int idLocalizacao = Convert.ToInt32(localizacaoId.ExecuteScalar());
                    return idLocalizacao;
                }
            }
        }

        internal void Remover(int id)
        {
            int idContato = buscarIdContato(id);
            int idLocalizacao = buscarIdLocalizacao(id);

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                string removerTabelaRestaurante = "DELETE FROM Restaurante WHERE Id = @Id";
                using (var removerRestaurante = new SqlCommand(removerTabelaRestaurante, connection))
                {
                    removerRestaurante.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    removerRestaurante.ExecuteNonQuery();
                }

                string removerTabelaContato = "DELETE FROM Contato WHERE Id = @Id";
                using (var removerContato = new SqlCommand(removerTabelaContato, connection))
                {
                    removerContato.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    removerContato.ExecuteNonQuery();
                }

                string removerTabelaLocalizacao = "DELETE FROM Localizacao WHERE Id = @Id";
                using (var removerLocalizacao = new SqlCommand(removerTabelaLocalizacao, connection))
                {
                    removerLocalizacao.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    removerLocalizacao.ExecuteNonQuery();
                }
            }
        #endregion

            #region Consulta
            #endregion

            #region Atualizar
            #endregion
        }
    }
} 