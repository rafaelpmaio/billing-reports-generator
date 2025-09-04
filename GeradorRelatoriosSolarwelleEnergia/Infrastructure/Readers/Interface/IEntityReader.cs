using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;

namespace GeradorRelatoriosSolarwelleEnergia.Infrastructure.Readers.Interface
{
    internal interface IEntityReader<T>
    {
        List<T> Read(string? filePath = null);
        //filePath é necessário apenas para o caso de arquivos externos (excel, json etc)
    }
}
