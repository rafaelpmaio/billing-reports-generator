using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorRelatoriosSolarwelleEnergia.Domain.Entities
{
    public class Endereco
    {
        public string Logradouro { get; set; } = "";
        public string Numero { get; set; } = "";
        public string Complemento { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Estado { get; set; } = "";
        public string Cep { get; set; } = "";
        public override string ToString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(Logradouro)) parts.Add(Logradouro);
            if (!string.IsNullOrWhiteSpace(Numero)) parts.Add($"Nº {Numero}");
            if (!string.IsNullOrWhiteSpace(Complemento)) parts.Add(Complemento);
            if (!string.IsNullOrWhiteSpace(Bairro)) parts.Add(Bairro);
            if (!string.IsNullOrWhiteSpace(Cidade)) parts.Add(Cidade);
            if (!string.IsNullOrWhiteSpace(Estado)) parts.Add(Estado);
            if (!string.IsNullOrWhiteSpace(Cep)) parts.Add($"CEP: {Cep}");

            return string.Join(",", parts);
        }
        public static Endereco Parse(string enderecoCompleto)
        {
            var endereco = new Endereco();
            if (string.IsNullOrWhiteSpace(enderecoCompleto))
                return endereco;

            var parts = enderecoCompleto.Split(',');
            try
            {
                if (parts.Length > 0) endereco.Logradouro = parts[0].Trim();

                if (parts.Length > 1)
                    endereco.Numero = parts[1].Trim().Replace("Nº ", "");

                if (parts.Length > 2)
                    endereco.Complemento = parts[2].Trim();

                if (parts.Length > 3)
                    endereco.Bairro = parts[3].Trim();

                if (parts.Length > 4)
                    endereco.Cidade = parts[4].Trim();

                if (parts.Length > 5)
                    endereco.Estado = parts[5].Trim();

                if (parts.Length > 6)
                    endereco.Cep = parts[6].Trim().Replace("CEP: ", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar o endereço:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return endereco;
        }

    }
}
