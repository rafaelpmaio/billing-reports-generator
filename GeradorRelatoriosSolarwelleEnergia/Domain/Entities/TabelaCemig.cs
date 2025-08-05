using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeOpenXml;
using ClosedXML.Excel;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Entities
{
    internal class TabelaCemig
    {
        public virtual string Modalidade { get; set; }
        public virtual string NumeroInstalacao { get; set; }
        public virtual string Periodo { get; set; }
        public virtual string Quota { get; set; }
        public virtual string PostoHorario { get; set; }
        public virtual string SaldoAnterior { get; set; }
        public virtual string SaldoExpirado { get; set; }
        public virtual string QtdConsumo { get; set; } 
        public virtual string QtdGeracao { get; set; } 
        public virtual string QtdCompensacao { get; set; } 
        public virtual string QtdTransfencia { get; set; } 
        public virtual string QtdRecebimento { get; set; } 
        public virtual string SaldoAtual { get; set; }
        public virtual string QtdSaldoExpirar { get; set; }
        public virtual string PeriodoSaldoExpirar { get; set; }
        
        public override string ToString()
        {
            return $"Modalidade: {Modalidade}, " +
                   $"Número de Instalação: {NumeroInstalacao}, " +
                   $"Período: {Periodo}, " +
                   $"Quota: {Quota}, " +
                   $"Posto Horário: {PostoHorario}, " +
                   $"Saldo Anterior: {SaldoAnterior}, " +
                   $"Saldo Expirado: {SaldoExpirado}, " +
                   $"Consumo: {QtdConsumo}, " +
                   $"Geração: {QtdGeracao}, " +
                   $"Compensação: {QtdCompensacao}, " +
                   $"Transferido: {QtdTransfencia}, " +
                   $"Recebimento: {QtdRecebimento}, " +
                   $"Saldo Atual: {SaldoAtual}, " +
                   $"Quantidade Saldo a Expirar: {QtdSaldoExpirar}, " +
                   $"Período Saldo a Expirar: {PeriodoSaldoExpirar}";
        }
    }
}

