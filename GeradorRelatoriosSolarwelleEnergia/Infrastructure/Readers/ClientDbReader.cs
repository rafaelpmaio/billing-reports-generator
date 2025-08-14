using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class ClientDbReader : IClientReader
    {
        private readonly ClientRepository _repository;

        public ClientDbReader()
        {
            _repository = new ClientRepository();
        }

        public List<Cliente> ReadClients(string? filePath = null)
        {
            return _repository.GetClients();
        }
    }
}
