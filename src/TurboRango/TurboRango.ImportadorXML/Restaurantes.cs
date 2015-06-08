using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        readonly static string SELECT_ALL= "SELECT [Capacidade], [Nome], [Categoria], [dbo].[Contato].[Site], [dbo].[Contato].[Telefone], [dbo].[Localizacao].[Bairro], [dbo].[Localizacao].[Latitude], [dbo].[Localizacao].[Logradouro], [dbo].[Localizacao].[Longitude] FROM [dbo].[Restaurante] INNER JOIN Contato ON [dbo].[Restaurante].[ContatoId] = [dbo].[Contato].[Id] INNER JOIN Localizacao ON [dbo].[Restaurante].[LocalizacaoId] = [dbo].[Localizacao].[Id]";
        readonly static string UPDATE_RESTAURANTE = "UPDATE [dbo].[Restaurante] SET [Capacidade] = @Capacidade, [Nome] = @Nome, [Categoria] = @Categoria WHERE [dbo].[Restaurante].[Id] = @Id";
        readonly static string UPDATE_CONTATO = "UPDATE [dbo].[Contato] SET [Site] = @Site, [Telefone] = @Telefone WHERE [Id] = @Id";
        readonly static string UPDATE_LOCALIZACAO = "UPDATE [dbo].[Localizacao] SET [Bairro] = @Bairro,[Logradouro] = @Logradouro, [Latitude] = @Latitude, [Longitude] = @Longitude WHERE [Id] = @Id";
        
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

        private int buscarIdContato(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                string selecionarContatoId = "SELECT [Id] FROM [dbo].[Contato] WHERE EXISTS (SELECT [ContatoId] FROM [dbo].[Restaurante] WHERE [dbo].[Restaurante].[Id] = @Id AND [dbo].[Restaurante].[ContatoId] = [dbo].[Contato].[Id])";
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
                string selecionarLocalicazaoId = "SELECT [Id] FROM [dbo].[Localizacao] WHERE EXISTS (SELECT [LocalizacaoId] FROM [dbo].[Restaurante] WHERE [dbo].[Restaurante].[Id] = @Id AND [dbo].[Restaurante].[LocalizacaoId] = [dbo].[Localizacao].[Id])";
                using (var localizacaoId = new SqlCommand(selecionarLocalicazaoId, connection))
                {
                    localizacaoId.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                    int idLocalizacao = Convert.ToInt32(localizacaoId.ExecuteScalar());
                    return idLocalizacao;
                }
            }
        }

        #region Remover
            internal void Remover(int id)
            {
                int idContato = buscarIdContato(id);
                int idLocalizacao = buscarIdLocalizacao(id);

                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    string removerTabelaRestaurante = "DELETE FROM [dbo].[Restaurante] WHERE [Id] = @Id";
                    using (var removerRestaurante = new SqlCommand(removerTabelaRestaurante, connection))
                    {
                        removerRestaurante.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                        removerRestaurante.ExecuteNonQuery();
                    }

                    string removerTabelaContato = "DELETE FROM [dbo].[Contato] WHERE [Id] = @Id";
                    using (var removerContato = new SqlCommand(removerTabelaContato, connection))
                    {
                        removerContato.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                        removerContato.ExecuteNonQuery();
                    }

                    string removerTabelaLocalizacao = "DELETE FROM [dbo].[Localizacao] WHERE [Id] = @Id";
                    using (var removerLocalizacao = new SqlCommand(removerTabelaLocalizacao, connection))
                    {
                        removerLocalizacao.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                        removerLocalizacao.ExecuteNonQuery();
                    }
                }
            }
            #endregion
            
        #region Consulta
            public IEnumerable<Restaurante> Todos()
            {
                List<Restaurante> restaurates = new List<Restaurante>();
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    using (var buscaRestaurantes = new SqlCommand(SELECT_ALL, connection))
                    {
                        connection.Open();
                        var reader = buscaRestaurantes.ExecuteReader();
                        while (reader.Read())
                        {
                            restaurates.Add(new Restaurante
                            {
                                Capacidade = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Categoria = (Categoria)Enum.Parse(typeof(Categoria), reader.GetString(2), ignoreCase: true),
                                Contato = new Contato
                                {
                                    Site = reader.GetString(3),
                                    Telefone = reader.GetString(4),
                                },
                                Localizacao = new Localizacao
                                {
                                    Bairro = reader.GetString(5),
                                    Latitude = reader.GetDouble(6),
                                    Logradouro = reader.GetString(7),
                                    Longitude = reader.GetDouble(8)
                                }
                            });
                        }
                    }
                    return restaurates;
                }
            }
        #endregion

        #region Atualizar
        internal void Atualizar(int id, Restaurante restaurante)
        {
            int idContato = buscarIdContato(id);
            int idLocalizacao = buscarIdLocalizacao(id);

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var atualizarRestaurante = new SqlCommand(UPDATE_RESTAURANTE, connection))
                {
                    atualizarRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    atualizarRestaurante.Parameters.Add("@Capacidade", SqlDbType.NVarChar).Value = restaurante.Capacidade;
                    atualizarRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;
                    connection.Open();
                    atualizarRestaurante.ExecuteNonQuery();
                }

                using (var atualizarContato = new SqlCommand(UPDATE_CONTATO, connection))
                {
                    atualizarContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = restaurante.Contato.Site ?? (object)DBNull.Value;
                    atualizarContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = restaurante.Contato.Telefone ?? (object)DBNull.Value;
                    connection.Open();
                    atualizarContato.ExecuteNonQuery();
                }

                using (var atualizarLocalizacao = new SqlCommand(UPDATE_LOCALIZACAO, connection))
                {
                    atualizarLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = restaurante.Localizacao.Bairro;
                    atualizarLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = restaurante.Localizacao.Logradouro;
                    atualizarLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = restaurante.Localizacao.Latitude;
                    atualizarLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = restaurante.Localizacao.Longitude;
                    atualizarLocalizacao.Parameters.Add("@Id", SqlDbType.NVarChar).Value = idLocalizacao;
                    connection.Open();
                    atualizarLocalizacao.ExecuteNonQuery();
                }
            }
        }
        #endregion

    }
}