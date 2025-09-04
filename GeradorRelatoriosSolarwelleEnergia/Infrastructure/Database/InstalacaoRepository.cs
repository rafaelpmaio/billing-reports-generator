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
    internal class InstalacaoRepository
    {
        private readonly string _connString = "Data Source=clients.db";

        public List<Instalacao> GetInstalacoes()
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
                    cmd.Parameters.AddWithValue("@NumeroInstalacao", instalacao.NumeroInstalacao);
                    cmd.Parameters.AddWithValue("@NumeroCliente", instalacao.NumeroCliente);
                    cmd.Parameters.AddWithValue("@DistribuidoraLocal", instalacao.DistribuidoraLocal);
                    cmd.Parameters.AddWithValue("@DescontoPercentual", instalacao.DescontoPercentual);                   
                    cmd.Parameters.AddWithValue("@Ativo", instalacao.Ativo);                   
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Instalacao> GetByInstalacao(string numeroInstalacao)
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
    }
}

