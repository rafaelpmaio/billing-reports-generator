using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class EnderecoImporter
    {
        private readonly EnderecoRepository _repository = new();
        private readonly EnderecoExcelReader _reader = new();

        public void ImportFromExcelToDb(string filePath)
        {
            var enderecos = _reader.Read(filePath);
            foreach (var endereco in enderecos)
            {
                _repository.Insert(endereco);
            }
            Console.WriteLine("Importação concluída com sucesso");
        }
    }
}

