using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database
{
    internal class EnderecoRepository
    {
        private readonly string _connString = "Data Source=clients.db";

        public int Insert(Endereco endereco)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var sql = @"INSERT INTO Enderecos (
                            Logradouro, 
                            Numero, 
                            Complemento, 
                            Bairro, 
                            Cidade, 
                            Estado, 
                            CEP
                           ) VALUES (
                            @Logradouro, 
                            @Numero, 
                            @Complemento, 
                            @Bairro, 
                            @Cidade, 
                            @Estado, 
                            @CEP);
                         SELECT last_insert_rowid();";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                    cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
                    cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
                    cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
                    cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
                    cmd.Parameters.AddWithValue("@Estado", endereco.Estado);
                    cmd.Parameters.AddWithValue("@CEP", endereco.Cep);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    

    public Endereco GetById(int id)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "SELECT * FROM Enderecos WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Endereco
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Logradouro = reader["Rua"].ToString(),
                                Numero = reader["Numero"].ToString(),
                                Complemento = reader["Complemento"].ToString(),
                                Bairro = reader["Bairro"].ToString(),
                                Cidade = reader["Cidade"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Cep = reader["CEP"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
    }

    //métodos update, delete, getall?
}
