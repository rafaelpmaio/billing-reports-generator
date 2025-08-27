using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database
{
    internal class InstalacaoRepository
    {
        private readonly string _connString = "Data Source=clients.db";

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
                            TipoCliente
                        ) VALUES (
                            @NumeroInstalacao,
                            @NumeroCliente,
                            @DistribuidoraLocal,
                            @DescontoPercentual,
                            @TipoCliente
                        );";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroInstalacao", instalacao.NumeroInstalacao);
                    cmd.Parameters.AddWithValue("@NumeroCliente", instalacao.NumeroCliente);
                    cmd.Parameters.AddWithValue("@DistribuidoraLocal", instalacao.DistribuidoraLocal);
                    cmd.Parameters.AddWithValue("@DescontoPercentual", instalacao.DescontoPercentual);
                    cmd.Parameters.AddWithValue("@TipoCliente", instalacao.TipoCliente);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Instalacao> GetByCliente(string numeroCliente)
        {
            var list = new List<Instalacao>();

            using (var conn = new SQLiteConnection(_connString))
            {
                conn.Open();
                var sql = "SELECT * FROM Instalacoes WHERE NumeroCliente = @NumeroCliente";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroCliente", numeroCliente);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var instalacao = new Instalacao
                            {
                                NumeroInstalacao = reader["NumeroInstalacao"].ToString(),
                                NumeroCliente = reader["NumeroCliente"].ToString(),
                                DistribuidoraLocal = reader["DistribuidoraLocal"].ToString(),
                                DescontoPercentual = reader["DescontoPercentual"].ToString(),
                                TipoCliente = Convert.ToInt32(reader["TipoCliente"])
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
}
