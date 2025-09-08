using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.Entities;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.DTO
{
    internal class ReportGenerationInputDto
    {
        public string CemigTablePath { get; set; }
        public List<Client> Clients {  get; set; }
        public List<Instalacao> Instalacoes { get; set; }
        public List<Endereco> Enderecos { get; set; }
        public float KwhValue { get; set; }
        public string PdfModelPath { get; set; }
        public string DestinyFolder {  get; set; }
        
    }
}
