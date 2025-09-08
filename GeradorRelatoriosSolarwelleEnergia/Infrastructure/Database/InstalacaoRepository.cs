using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database
{
    internal class InstalacaoRepository
    {
        private readonly string _connString = "Data Source=instalacoes.db";

        public InstalacaoRepository()
        {
            CreateDB();
        }
        private void CreateDB()
        {
            if (!File.Exists("instalacoes.db"))
            {
                SQLiteConnection.CreateFile("instalacoes.db");

                using (var conn = new SQLiteConnection(_connString))
                {
                    conn.Open();

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
                        cmd.CommandText = sqlInstalacoes;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<Instalacao> GetAll()
        {
            var list = new List<Instalacao>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Instalacoes", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Instalacao instalacao = new Instalacao();

                        instalacao.NumeroInstalacao = reader["NumeroInstalacao"]?.ToString();
                        instalacao.NumeroCliente = reader["NumeroCliente"]?.ToString();
                        instalacao.DistribuidoraLocal = reader["DistribuidoraLocal"]?.ToString();
                        instalacao.DescontoPercentual = reader["DescontoPercentual"]?.ToString();
                        instalacao.Ativo = reader["Ativo"]?.ToString().Trim() == "1";

                        list.Add(instalacao);
                    }
                }
            }
            return list;
        }
        public void Insert(Instalacao instalacao)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var sql = @"INSERT INTO Instalacoes (
                            NumeroInstalacao,
                            NumeroCliente,
                            DistribuidoraLocal,
                            DescontoPercentual,                            
                            Ativo                            
                        ) VALUES (
                            @NumeroInstalacao,
                            @NumeroCliente,
                            @DistribuidoraLocal,
                            @DescontoPercentual,                         
                            @Ativo                         
                        );";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    AddInstalacaoParameters(cmd, instalacao);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Instalacao instalacao)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = @"UPDATE Instalacoes SET
                        NumeroInstalacao = @NumeroInstalacao,
                        NumeroCliente = @NumeroCliente,
                        DistribuidoraLocal = @DistribuidoraLocal,
                        DescontoPercentual = @DescontoPercentual,
                        Ativo = @Ativo
                    WHERE NumeroInstalacao = @NumeroInstalacao";

                var cmd = new SQLiteCommand(sql, conn);
                AddInstalacaoParameters(cmd, instalacao);
                cmd.ExecuteNonQuery();
            }
        }
        public void Remove(string numeroInstalacao)
        {
            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                string sql = "UPDATE Instalacoes SET Ativo = 0 WHERE NumeroInstalacao = @numeroInstalacao";
                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@numeroInstalacao", numeroInstalacao);
                cmd.ExecuteNonQuery();
            }
        }
        public List<Instalacao> GetByNumeroinstalacao(string numeroInstalacao)
        {
            var list = new List<Instalacao>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var sql = "SELECT * FROM Instalacoes WHERE NumeroInstalacao = @NumeroInstalacao";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroInstalacao", numeroInstalacao);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var instalacao = new Instalacao
                            {
                                NumeroInstalacao = reader["NumeroInstalacao"].ToString(),
                                NumeroCliente = reader["NumeroCliente"].ToString(),
                                DistribuidoraLocal = reader["DistribuidoraLocal"].ToString(),
                                DescontoPercentual = reader["DescontoPercentual"].ToString()
                            };
                            list.Add(instalacao);
                        }
                    }
                }
            }
            return list;
        }
        private void AddInstalacaoParameters(SQLiteCommand cmd, Instalacao instalacao)
        {
            cmd.Parameters.AddWithValue("@NumeroInstalacao", instalacao.NumeroInstalacao);
            cmd.Parameters.AddWithValue("@NumeroCliente", instalacao.NumeroCliente);
            cmd.Parameters.AddWithValue("@DistribuidoraLocal", instalacao.DistribuidoraLocal);
            cmd.Parameters.AddWithValue("@DescontoPercentual", instalacao.DescontoPercentual);
            cmd.Parameters.AddWithValue("@Ativo", instalacao.Ativo);
        }
    }
}

