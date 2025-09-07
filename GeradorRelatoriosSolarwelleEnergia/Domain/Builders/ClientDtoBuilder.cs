using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeradorRelatoriosSolarwelleEnergia.Domain.DTO;
using GeradorRelatoriosSolarwelleEnergia.Dominio.Entidades;
using OfficeOpenXml;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Builders
{
    internal class ClientDtoBuilder
    {
        public static ClientDto FromClient(Client client)
        {
            return new ClientDto
            {
                NumeroCliente = client.NumeroCliente,
                Instalacoes = string.Join(", ", client.Instalacoes),
                RazaoSocialOuNome = client is ClientePessoaJuridica pj ? pj.RazaoSocial : ((ClientePessoaFisica)client).Nome,
                CnpjOuCpf = client is ClientePessoaJuridica pj2 ? pj2.Cnpj : ((ClientePessoaFisica)client).Cpf,
                RepresentanteLegal = client is ClientePessoaJuridica pj3 ? pj3.RepresentanteLegal : null,
                Rg = client is ClientePessoaFisica pf ? pf.Rg : null,
                Telefone = client.Telefone,
                IdEndereco = client.IdEndereco,
                Email = client.Email,
                TipoCliente = client.TipoCliente,
                Ativo = client.Ativo
            };
        }

        public static void PreencherDadosPjOuPf(ClientDto dto, Client client)
        {
            if (client is ClientePessoaJuridica pj)
            {
                dto.RazaoSocialOuNome = pj.RazaoSocial;
                dto.CnpjOuCpf = pj.Cnpj;
                dto.RepresentanteLegal = pj.RepresentanteLegal;
                dto.TipoCliente = 1;
            }
            else if (client is ClientePessoaFisica pf)
            {
                dto.RazaoSocialOuNome = pf.Nome;
                dto.CnpjOuCpf = pf.Cpf;
                dto.Rg = pf.Rg;
                dto.TipoCliente = 0;
            }
        }
        public static void PreeencherDadosPjOuPf(Client client, string nomeOuRazaoSocial, string cpfOuCnpj, string representanteLegal,string rg)
        {
            if (client is ClientePessoaJuridica pj)
            {
                pj.RazaoSocial = nomeOuRazaoSocial;
                pj.Cnpj = cpfOuCnpj;
                pj.RepresentanteLegal = representanteLegal;
            }
            else if (client is ClientePessoaFisica pf)
            {
                pf.Nome = nomeOuRazaoSocial;
                pf.Cpf = cpfOuCnpj;
                pf.Rg = rg;
            }
        }

    }
}
