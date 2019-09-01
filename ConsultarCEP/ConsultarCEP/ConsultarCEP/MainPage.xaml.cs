using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultarCEP.Objetos;
using ConsultarCEP.Servico;

namespace ConsultarCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BTBUSCAR.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = ENCEP.Text.Trim();
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco endereco = ViaCepWS.BuscarEnderecoViaCEP(cep);
                    if(endereco == null)
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: "+cep+"!", "OK");
                    }
                    else
                    {
                        LBRESULTADO.Text = string.Format("Endereço: {0}, {1}, {2} {3}",
                            endereco.logradouro, endereco.bairro, endereco.localidade, endereco.uf);
                    }
                }
                catch(Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
            else
            {

            }
        }

        private bool isValidCEP(String cep)
        {
            bool valido = true;
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }                
            int novoCep = 0;
            if (!int.TryParse(cep, out novoCep))
            {
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter apenas números.", "OK");
                valido = false;
            }                
            return valido;
        }
    }
}
