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
                            NumeroCliente,
                            NumeroInstalacao,
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
                            @NumeroCliente,
                            @NumeroInstalacao,
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

                string razaoSocialOuNome = "";
                string cnpjOuCpf = "";
                string representanteLegal = "";
                string Rg = "";
                int tipoCliente = 0;

                cmd.Parameters.AddWithValue("@NumeroCliente", cliente.NumeroCliente);
                cmd.Parameters.AddWithValue("@NumeroInstalacao", cliente.NumeroInstalacao);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? "");
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? "");
                cmd.Parameters.AddWithValue("@Email", cliente.Email ?? "");
                cmd.Parameters.AddWithValue("@DistribuidoraLocal", cliente.DistribuidoraLocal ?? "");
                cmd.Parameters.AddWithValue("@DescontoPercentual", cliente.DescontoPercentual ?? "");

                if (cliente is ClientePessoaJuridica pj)
                {
                    razaoSocialOuNome = pj.RazaoSocial ?? "";
                    cnpjOuCpf = pj.Cnpj ?? "";
                    representanteLegal = pj.RepresentanteLegal ?? "";
                    tipoCliente = 1;

                }
                else if (cliente is ClientePessoaFisica pf)
                {
                    razaoSocialOuNome = pf.Nome ?? "";
                    cnpjOuCpf = pf.Cpf ?? "";
                    Rg = pf.Rg;
                    tipoCliente = 0;
                }

                cmd.Parameters.AddWithValue("@RazaoSocialOuNome", razaoSocialOuNome);
                cmd.Parameters.AddWithValue("@CnpjOuCpf", cnpjOuCpf);
                cmd.Parameters.AddWithValue("@RepresentanteLegal", representanteLegal);
                cmd.Parameters.AddWithValue("@Rg", Rg);
                cmd.Parameters.AddWithValue("@TipoCliente", tipoCliente);

                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM Clientes WHERE NumeroCliente = @numeroCliente";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@numeroCliente", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
