using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ConsultarCEP.Objetos;
using Newtonsoft.Json;

namespace ConsultarCEP.Servico
{
    public class ViaCepWS
    {
        private static string EnderecoURL = "https://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCEP(string Cep)
        {
            string NovoEnderecoURL = string.Format(EnderecoURL, Cep);

            WebClient ws = new WebClient();
            string Conteudo = ws.DownloadString(NovoEnderecoURL);

            Endereco endereco = JsonConvert.DeserializeObject<Endereco>(Conteudo);

            if (String.IsNullOrEmpty(endereco.cep))
                return null;

            return endereco;
        }
    }
}
