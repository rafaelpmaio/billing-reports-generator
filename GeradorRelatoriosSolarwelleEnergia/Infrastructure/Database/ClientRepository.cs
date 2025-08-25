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
                    string sql = @"CREATE TABLE Clientes (                                    
                                    NumeroInstalacao TEXT PRIMARY KEY,
                                    NumeroCliente TEXT,
                                    RazaoSocialOuNome TEXT,
                                    CnpjOuCpf TEXT,
                                    RepresentanteLegal TEXT NULL,   
                                    Rg TEXT NULL,
                                    Telefone TEXT NULL,
                                    Endereco TEXT NULL,
                                    Email TEXT NULL,
                                    DistribuidoraLocal TEXT,
                                    DescontoPercentual TEXT,
                                    TipoCliente INTEGER
                                   );";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
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
                        cliente.NumeroInstalacao = reader["NumeroInstalacao"]?.ToString();
                        cliente.Telefone = reader["Telefone"]?.ToString();
                        cliente.Endereco = reader["Endereco"]?.ToString();
                        cliente.Email = reader["Email"]?.ToString();
                        cliente.DistribuidoraLocal = reader["DistribuidoraLocal"]?.ToString();
                        cliente.DescontoPercentual = reader["DescontoPercentual"]?.ToString();

                        if (cliente is ClientePessoaJuridica pj)
                        {
                            pj.RazaoSocial = reader["RazaoSocialOuNome"]?.ToString();
                            pj.Cnpj = reader["CnpjOuCpf"]?.ToString();
                            pj.RepresentanteLegal = reader["RepresentanteLegal"]?.ToString();
                        }
                        else if (cliente is ClientePessoaFisica pf)
                        {
                            pf.Nome = reader["RazaoSocialOuNome"]?.ToString();
                            pf.Cpf = reader["CnpjOuCpf"]?.ToString();
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
                            NumeroInstalacao,
                            NumeroCliente,
                            Telefone,
                            Endereco,
                            Email,
                            DistribuidoraLocal,
                            DescontoPercentual,
                            RazaoSocialOuNome,
                            CnpjOuCpf,
                            Rg,
                            RepresentanteLegal,
                            TipoCliente
                        ) VALUES (
                            @NumeroInstalacao,
                            @NumeroCliente,
                            @Telefone,
                            @Endereco,
                            @Email,
                            @DistribuidoraLocal,
                            @DescontoPercentual,
                            @RazaoSocialOuNome,
                            @CnpjOuCpf,
                            @Rg,
                            @RepresentanteLegal,
                            @TipoCliente
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
                        Telefone = @Telefone,
                        Endereco = @Endereco,
                        Email = @Email,
                        DistribuidoraLocal = @DistribuidoraLocal,
                        DescontoPercentual = @DescontoPercentual,
                        RazaoSocialOuNome = @RazaoSocialOuNome,
                        CnpjOuCpf = @CnpjOuCpf,
                        Rg = @Rg,
                        RepresentanteLegal = @RepresentanteLegal,
                        TipoCliente = @TipoCliente
                    WHERE NumeroInstalacao = @NumeroInstalacao";

                var cmd = new SQLiteCommand(sql, conn);
                AddClientParameters(cmd, cliente);             
                cmd.ExecuteNonQuery();
            }
        }
        public void Remove(string numeroInstalacao)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM Clientes WHERE NumeroInstalacao  = @numeroInstalacao";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@numeroInstalacao", numeroInstalacao);
                cmd.ExecuteNonQuery();
            }
        }
        private void AddClientParameters(SQLiteCommand cmd, Cliente client, bool includeNumeroInstalacao = true)
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

            if (includeNumeroInstalacao)
                cmd.Parameters.AddWithValue("@NumeroInstalacao", client.NumeroInstalacao);

            cmd.Parameters.AddWithValue("@NumeroCliente", client.NumeroCliente);
            cmd.Parameters.AddWithValue("@Telefone", client.Telefone ?? "");
            cmd.Parameters.AddWithValue("@Endereco", client.Endereco ?? "");
            cmd.Parameters.AddWithValue("@Email", client.Email ?? "");
            cmd.Parameters.AddWithValue("@DistribuidoraLocal", client.DistribuidoraLocal ?? "");
            cmd.Parameters.AddWithValue("@DescontoPercentual", client.DescontoPercentual ?? "");
            cmd.Parameters.AddWithValue("@RazaoSocialOuNome", razaoSocialOuNome);
            cmd.Parameters.AddWithValue("@CnpjOuCpf", cnpjOuCpf);
            cmd.Parameters.AddWithValue("@RepresentanteLegal", representanteLegal);
            cmd.Parameters.AddWithValue("@Rg", Rg);
            cmd.Parameters.AddWithValue("@TipoCliente", tipoCliente);

        }
    }
}
