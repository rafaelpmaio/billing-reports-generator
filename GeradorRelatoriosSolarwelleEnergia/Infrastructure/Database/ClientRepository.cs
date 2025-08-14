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
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    NumeroCliente TEXT,
                                    NumeroInstalacao TEXT,
                                    RazaoSocialOuNome TEXT,
                                    CnpjOuCpf TEXT,
                                    RepresentanteLegal TEXT NULL,   
                                    RG TEXT NULL,
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
                        //IMPLEMENTAR CLIENT ROW MAPPER ADAPTADO
                        list.Add(new Cliente
                        {
                            NumeroCliente = "1",
                            DescontoPercentual = "1",
                        });
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
                            @RepresentanteLegal,
                            @TipoCliente
                        );";

                var cmd = new SQLiteCommand(sql, conn);

                cmd.Parameters.AddWithValue("@NumeroCliente", cliente.NumeroCliente);
                cmd.Parameters.AddWithValue("@NumeroInstalacao", cliente.NumeroInstalacoes);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? "");
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? "");
                cmd.Parameters.AddWithValue("@Email", cliente.Email ?? "");
                cmd.Parameters.AddWithValue("@DistribuidoraLocal", cliente.DistribuidoraLocal ?? "");
                cmd.Parameters.AddWithValue("@DescontoPercentual", cliente.DescontoPercentual ?? "");

                if (cliente is ClientePessoaJuridica pj)
                {
                    cmd.Parameters.AddWithValue("@RazaoSocialOuNome", pj.RazaoSocial ?? "");
                    cmd.Parameters.AddWithValue("@CnpjOuCpf", pj.Cnpj ?? "");
                    cmd.Parameters.AddWithValue("@RepresentanteLegal", pj.RepresentanteLegal ?? "");
                    cmd.Parameters.AddWithValue("@TipoCliente", 1);

                }
                else if (cliente is ClientePessoaFisica pf)
                {
                    cmd.Parameters.AddWithValue("@RazaoSocialOuNome", pf.Nome ?? "");
                    cmd.Parameters.AddWithValue("@CnpjOuCpf", pf.Cpf ?? "");
                    cmd.Parameters.AddWithValue("@TipoCliente", 0);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM Clientes WHERE Id = @id";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
