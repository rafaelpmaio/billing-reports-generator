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

        public static Client ToClient(ClientDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Client client;

            if (dto.TipoCliente == 1)
            {
                client = new ClientePessoaJuridica
                {
                    RazaoSocial = dto.RazaoSocialOuNome,
                    Cnpj = dto.CnpjOuCpf,
                    RepresentanteLegal = dto.RepresentanteLegal
                };
            }
            else
            {
                client = new ClientePessoaFisica
                {
                    Nome = dto.RazaoSocialOuNome,
                    Cpf = dto.CnpjOuCpf,
                    Rg = dto.Rg
                };
            }
            client.NumeroCliente = dto.NumeroCliente;
            client.Telefone = dto.Telefone;
            client.Email = dto.Email;
            client.IdEndereco = dto.IdEndereco;
            client.Ativo = dto.Ativo;
            client.TipoCliente = dto.TipoCliente;

            client.Instalacoes = (dto.Instalacoes ?? "")
                .Split(',')
                .Select(i => i.Trim())
                .Where(i => !string.IsNullOrEmpty(i))
                .ToArray();

            client.InstalacoesString = dto.Instalacoes;

            return client;
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

    }
}
