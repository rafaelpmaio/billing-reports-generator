using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database
{
    internal class EnderecoRepository
    {
        private readonly string _connString = "Data Source=enderecos.db";

        public EnderecoRepository()
        {
            CreateDB();
        }

        private void CreateDB()
        {
            if (!File.Exists("enderecos.db"))
            {
                SQLiteConnection.CreateFile("enderecos.db");

                using (var conn = new SQLiteConnection(_connString))
                {
                    conn.Open();

                    string sqlEnderecos = @"CREATE TABLE Enderecos (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Logradouro TEXT NOT NULL,
                                    Numero TEXT,
                                    Complemento TEXT,
                                    Bairro TEXT NOT NULL,
                                    Cidade TEXT NOT NULL,
                                    Estado TEXT NOT NULL,
                                    CEP TEXT NOT NULL
                                   );";

                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = sqlEnderecos;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<Endereco> GetAll()
        {
            var list = new List<Endereco>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Enderecos", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Endereco endereco = new Endereco();

                        endereco.Id = Convert.ToInt32(reader["Id"]);
                        endereco.Logradouro = reader["Logradouro"]?.ToString();
                        endereco.Numero = reader["Numero"]?.ToString();
                        endereco.Complemento = reader["Complemento"]?.ToString();
                        endereco.Bairro = reader["Bairro"]?.ToString();
                        endereco.Cidade = reader["Cidade"]?.ToString();
                        endereco.Estado = reader["Estado"]?.ToString();
                        endereco.Cep = reader["CEP"]?.ToString();

                        list.Add(endereco);
                    }
                }
            }
            return list;
        }
        public int Insert(Endereco endereco)
        {
            int nextId = GetNextId();
            endereco.Id = nextId;

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
                    AddEnderecoParameters(cmd, endereco);
                    cmd.ExecuteNonQuery();
                }
                return endereco.Id;
            }
        }
        public void Update(Endereco endereco)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();

                var sql = @"UPDATE Enderecos SET
                        Logradouro = @Logradouro,
                        Numero = @Numero,
                        Complemento = @Complemento,
                        Bairro = @Bairro,
                        Cidade = @Cidade,
                        Estado = @Estado,
                        CEP = @CEP
                    WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    AddEnderecoParameters(cmd, endereco);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();

                var sql = "DELETE FROM Enderecos WHERE Id = @Id";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
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
                                Logradouro = reader["Logradouro"].ToString(),
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
        public int GetNextId()
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT MAX(Id) FROM Enderecos", conn);
                var result = cmd.ExecuteScalar();
                return (result != DBNull.Value && int.TryParse(result.ToString(), out int maxId)) ? maxId + 1 : 1;
            }
        }
        private void AddEnderecoParameters(SQLiteCommand cmd, Endereco endereco)
        {
            cmd.Parameters.AddWithValue("@Id", endereco.Id);
            cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
            cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
            cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
            cmd.Parameters.AddWithValue("@Bairro", endereco.Bairro);
            cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
            cmd.Parameters.AddWithValue("@Estado", endereco.Estado);
            cmd.Parameters.AddWithValue("@CEP", endereco.Cep);
        }
    }
}
