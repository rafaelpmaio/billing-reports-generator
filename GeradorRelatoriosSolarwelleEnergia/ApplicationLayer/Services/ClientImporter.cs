using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers;

namespace GeradorRelatoriosSolarwelleEnergia.ApplicationLayer.Services
{
    internal class ClientImporter
    {
        private readonly ClientRepository _repository = new();
        private readonly ClientExcelReader _reader = new();

        public void ImportFromExcelToDb(string filePath)
        {
            var clients = _reader.ReadClients(filePath);
            foreach (var client in clients)
            {               
                _repository.Insert(client);
            }
            Console.WriteLine("Importação concluída com sucesso");
        }
    }
}
