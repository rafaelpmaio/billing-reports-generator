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
                                    Telefone TEXT,
                                    Endereco TEXT,
                                    Email TEXT,
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

        public void Insert(Cliente cliente, RelatorioCliente clientReport)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = @"INSERT INTO Clientes (
                                NumeroCliente, NumeroInstalacao, Telefone, Endereco, Email,
                                DistribuidoraLocal, DescontoPercentual, RazaoSocialOuNome,
                                CnpjOuCpf, NumeroRG, TipoCliente)
                                VALUES (
                                @NumeroCliente, @NumeroInstalacao, @Telefone, @Endereco, @Email,
                                @DistribuidoraLocal, @DescontoPercentual, @RazaoSocialOuNome,
                                @CnpjOuCpf, @NumeroRG, @TipoCliente);";
                var cmd = new SQLiteCommand(sql,conn);
                cmd.Parameters.AddWithValue("@NumeroCliente", cliente.NumeroCliente);
                cmd.Parameters.AddWithValue("@NumeroInstalacao", cliente.NumeroInstalacoes);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@DistribuidoraLocal", cliente.DistribuidoraLocal);
                cmd.Parameters.AddWithValue("@DescontoPercentual", cliente.DescontoPercentual);
                cmd.Parameters.AddWithValue("@RazaoSocialOuNome", clientReport.RazaoSocialOuNome);
                cmd.Parameters.AddWithValue("@CnpjOuCpf", clientReport.CnpjOuCpf);
                //cmd.Parameters.AddWithValue("@NumeroRG", clientReport.rg);
                cmd.Parameters.AddWithValue("@TipoCliente", clientReport.TipoCliente);

                cmd.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "DELETE FROM Clientes WHERE Id = @id";
                var cmd = new SQLiteCommand(sql,conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
