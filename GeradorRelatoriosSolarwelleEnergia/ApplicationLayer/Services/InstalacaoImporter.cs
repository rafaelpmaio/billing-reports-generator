using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class InstalacaoImporter
    {
        private readonly InstalacaoRepository _repository = new();
        private readonly InstalacaoExcelReader _reader = new();

        public void ImportFromExcelToDb(string filePath)
        {
            var instalacoes = _reader.Read(filePath);
            foreach (var instalacao in instalacoes)
            {
                _repository.Insert(instalacao);
            }
            Console.WriteLine("Importação concluída com sucesso");
        }
    }
}
