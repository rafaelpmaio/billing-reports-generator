using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Builders;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using Org.BouncyCastle.Pqc.Crypto.Cmce;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database
{
    internal class ClientRepository
    {
        private readonly string _connString = "Data Source=clients.db";

        public ClientRepository()
        {
            CreateDB();
        }
        private void CreateDB()
        {
            if (!File.Exists("clients.db"))
            {
                SQLiteConnection.CreateFile("clients.db");

                using (var conn = new SQLiteConnection(_connString))
                {
                    conn.Open();

                    string sqlClientes = @"CREATE TABLE Clientes (
                                    NumeroCliente TEXT PRIMARY KEY,
                                    Instalacoes TEXT,
                                    RazaoSocialOuNome TEXT NOT NULL,
                                    CnpjOuCpf TEXT NOT NULL,
                                    RepresentanteLegal TEXT,
                                    Rg TEXT,
                                    Telefone TEXT,
                                    IdEndereco INTEGER,
                                    Email TEXT,
                                    TipoCliente INTEGER,
                                    Ativo INTEGER,
                                    FOREIGN KEY (IdEndereco) REFERENCES Enderecos(Id)
                                   );";

                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = sqlClientes;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<Client> GetAll()
        {
            var list = new List<Client>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Clientes", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dto = new ClientDto
                        {
                            NumeroCliente = reader["NumeroCliente"]?.ToString(),
                            Instalacoes = reader["Instalacoes"]?.ToString(),
                            RazaoSocialOuNome = reader["RazaoSocialOuNome"]?.ToString(),
                            CnpjOuCpf = reader["CnpjOuCpf"]?.ToString(),
                            RepresentanteLegal = reader["RepresentanteLegal"]?.ToString(),
                            Rg = reader["RG"]?.ToString(),
                            Telefone = reader["Telefone"]?.ToString(),
                            IdEndereco = Convert.ToInt32(reader["IdEndereco"]),
                            Email = reader["Email"]?.ToString(),
                            TipoCliente = reader.GetInt32(reader.GetOrdinal("TipoCliente")),
                            Ativo = Convert.ToInt32(reader["Ativo"]) == 1 ? true : false
                        };

                        var client = ClientDtoBuilder.ToClient(dto);
                        list.Add(client);
                    }
                }
            }
            return list;
        }
        public void Insert(ClientDto dto)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = @"INSERT INTO Clientes (
                                NumeroCliente,
                                Instalacoes,
                                RazaoSocialOuNome,
                                CnpjOuCpf,
                                RepresentanteLegal,
                                Rg,
                                Telefone,
                                IdEndereco,
                                Email,
                                TipoCliente,
                                Ativo
                             ) VALUES (
                                @NumeroCliente,
                                @Instalacoes,
                                @RazaoSocialOuNome,
                                @CnpjOuCpf,
                                @RepresentanteLegal,
                                @Rg,
                                @Telefone,
                                @IdEndereco,
                                @Email,
                                @TipoCliente,
                                @Ativo
                              );";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    AddClientParameters(cmd, dto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(ClientDto dto)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = @"UPDATE Clientes SET
                        NumeroCliente = @NumeroCliente,
                        Instalacoes = @Instalacoes,
                        RazaoSocialOuNome = @RazaoSocialOuNome,
                        CnpjOuCpf = @CnpjOuCpf,
                        RepresentanteLegal = @RepresentanteLegal,
                        Rg = @Rg,
                        Telefone = @Telefone,
                        IdEndereco = @IdEndereco,
                        Email = @Email,
                        TipoCliente = @TipoCliente,
                        Ativo = @Ativo
                    WHERE NumeroCliente = @NumeroCliente";

                var cmd = new SQLiteCommand(sql, conn);
                AddClientParameters(cmd, dto);
                cmd.ExecuteNonQuery();
            }
        }
        public void Remove(string numeroCliente)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "UPDATE Clientes SET Ativo = 0 WHERE NumeroCliente = @numeroCliente";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@numeroCliente", numeroCliente);
                cmd.ExecuteNonQuery();
            }
        }
        private void AddClientParameters(SQLiteCommand cmd, ClientDto dto)
        {
            cmd.Parameters.AddWithValue("@NumeroCliente", dto.NumeroCliente);
            cmd.Parameters.AddWithValue("@Instalacoes", dto.Instalacoes);
            cmd.Parameters.AddWithValue("@RazaoSocialOuNome", dto.RazaoSocialOuNome);
            cmd.Parameters.AddWithValue("@CnpjOuCpf", dto.CnpjOuCpf);
            cmd.Parameters.AddWithValue("@RepresentanteLegal", dto.RepresentanteLegal ?? "");
            cmd.Parameters.AddWithValue("@Rg", dto.Rg ?? "");
            cmd.Parameters.AddWithValue("@Telefone", dto.Telefone ?? "");
            cmd.Parameters.AddWithValue("@IdEndereco", dto.IdEndereco);
            cmd.Parameters.AddWithValue("@Email", dto.Email ?? "");
            cmd.Parameters.AddWithValue("@TipoCliente", dto.TipoCliente);
            cmd.Parameters.AddWithValue("@Ativo", dto.Ativo ? 1 : 0);
        }
    }
}

