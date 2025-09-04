using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Database;
using GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers.Interface;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers
{
    internal class ClientDbReader : IEntityReader<Client>
    {
        private readonly ClientRepository _repository;

        public ClientDbReader()
        {
            _repository = new ClientRepository();
        }

        public List<Client> Read(string? filePath = null)
        {
            return _repository.GetClients();
        }
    }
}
