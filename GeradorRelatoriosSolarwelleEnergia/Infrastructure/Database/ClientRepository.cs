using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
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

                    string sqlInstalacoes = @"CREATE TABLE Instalacoes (
                                    NumeroInstalacao TEXT PRIMARY KEY,
                                    NumeroCliente TEXT NOT NULL,
                                    DistribuidoraLocal TEXT NOT NULL,
                                    DescontoPercentual TEXT,
                                    Ativo INTEGER NOT NULL,
                                    FOREIGN KEY (NumeroCliente) REFERENCES Clientes(NumeroCliente)
                                   );";

                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = sqlEnderecos;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = sqlClientes;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = sqlInstalacoes;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<Cliente> GetClients()
        {
            var list = new List<Cliente>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Clientes", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int tipoCliente = reader.GetInt32(reader.GetOrdinal("TipoCliente"));

                        Cliente cliente = tipoCliente == 1
                            ? new ClientePessoaJuridica()
                            : new ClientePessoaFisica();

                        cliente.NumeroCliente = reader["NumeroCliente"]?.ToString();
                        string rawInstalacoes = reader["Instalacoes"]?.ToString();
                        string razaoSocialOuNome = reader["RazaoSocialOuNome"]?.ToString();
                        string cpfOuCnpj = reader["CnpjOuCpf"]?.ToString();
                        cliente.Telefone = reader["Telefone"]?.ToString();
                        cliente.IdEndereco = Convert.ToInt32(reader["IdEndereco"]);
                        cliente.Email = reader["Email"]?.ToString();
                        cliente.TipoCliente = tipoCliente;                    
                        cliente.Ativo = Convert.ToInt32(reader["Ativo"]) == 1 ? true : false;
                                                                      
                        cliente.Instalacoes = string.IsNullOrWhiteSpace(rawInstalacoes)
                            ? Array.Empty<string>()
                            : rawInstalacoes.Split(',').Select(i => i.Trim()).ToArray();

                        cliente.InstalacoesString = string.Join(", ", cliente.Instalacoes);

                        if (cliente is ClientePessoaJuridica pj)
                        {
                            pj.RazaoSocial = razaoSocialOuNome;
                            pj.Cnpj = cpfOuCnpj;
                            pj.RepresentanteLegal = reader["RepresentanteLegal"]?.ToString();
                        }
                        else if (cliente is ClientePessoaFisica pf)
                        {
                            pf.Nome = razaoSocialOuNome;
                            pf.Cpf = cpfOuCnpj;
                            pf.Rg = reader["RG"]?.ToString();
                        }
                        list.Add(cliente);
                    }
                }
            }
            return list;
        }
        public void Insert(Cliente cliente)
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

                var cmd = new SQLiteCommand(sql, conn);
                AddClientParameters(cmd, cliente);                              

                cmd.ExecuteNonQuery();
            }
        }        
        public void Update(Cliente cliente)
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
                AddClientParameters(cmd, cliente);
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
        private void AddClientParameters(SQLiteCommand cmd, Cliente client)
        {
            string razaoSocialOuNome = "";
            string cnpjOuCpf = "";
            string representanteLegal = "";
            string Rg = "";
            int tipoCliente = 0;

            if (client is ClientePessoaJuridica pj)
            {
                razaoSocialOuNome = pj.RazaoSocial ?? "";
                cnpjOuCpf = pj.Cnpj ?? "";
                representanteLegal = pj.RepresentanteLegal ?? "";
                tipoCliente = 1;

            }
            else if (client is ClientePessoaFisica pf)
            {
                razaoSocialOuNome = pf.Nome ?? "";
                cnpjOuCpf = pf.Cpf ?? "";
                Rg = pf.Rg;
                tipoCliente = 0;
            }

            string instalacoesString = string.Join(",", client.Instalacoes);

            cmd.Parameters.AddWithValue("@NumeroCliente", client.NumeroCliente);
            cmd.Parameters.AddWithValue("@Instalacoes", instalacoesString);
            cmd.Parameters.AddWithValue("@RazaoSocialOuNome", razaoSocialOuNome);
            cmd.Parameters.AddWithValue("@CnpjOuCpf", cnpjOuCpf);
            cmd.Parameters.AddWithValue("@RepresentanteLegal", representanteLegal);
            cmd.Parameters.AddWithValue("@Rg", Rg);
            cmd.Parameters.AddWithValue("@Telefone", client.Telefone ?? "");
            cmd.Parameters.AddWithValue("@IdEndereco", client.IdEndereco);
            cmd.Parameters.AddWithValue("@Email", client.Email ?? "");
            cmd.Parameters.AddWithValue("@TipoCliente", tipoCliente);
            cmd.Parameters.AddWithValue("@Ativo", client.Ativo ? 1 : 0);
        }
    }
}
