using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text)) // verifica se o campo de texto não está vazio 
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);
                    // chama o método GetPrevisao da classe DataService, passando o nome da cidade como parâmetro

                    if (t != null) // verifica se o objeto Tempo não é nulo
                    {
                        string dados_previsao = $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nascer do Sol: {t.sunrise}\n" +
                            $"Pôr do Sol: {t.sunset}\n" +
                            $"Temp Máx: {t.temp_max}°C\n" +
                            $"Temp Mín: {t.temp_min}°C\n" +
                            $"Velocidade do Vento: {t.speed} m/s\n" + // adicionado para exibir a velocidade do vento
                            $"Visibilidade: {t.visibility} m\n" + // adicionado para exibir a visibilidade
                            $"Descrição: {t.description}\n"; // adicionado para exibir a descrição do tempo

                        lbl_res.Text = dados_previsao; // adiciona os dados de previsão à label no front-end
                    }
                    else // senão
                    {
                        lbl_res.Text = "Cidade não encontrada"; // adicionado para exibir mensagem de cidade não encontrada
                    }
                }
                else // senão
                {
                    lbl_res.Text = "Digite o nome de uma cidade"; // adicionado para exibir mensagem de campo vazio 
                }

            } // fecha try


            catch (Exception ex) // para capturar exceções
            {


                // os blocos abaixo são exibidas caso não tenha conexão com a internet

                if (ex.Message == "No such host is known. (api.openweathermap.org:443)")
                // verifica se a mensagem de erro é a mesma que a do servidor
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                    // adicionado para exibir mensagem de erro caso não tenha conexão com a internet
                }
                else if (ex.Message == "Connection failure")
                // verifica se a mensagem de erro é a mesma que a do servidor
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                    // adicionado para exibir mensagem de erro caso não tenha conexão com a internet        
                }
                else if (ex.Message == "Unable to resolve host \"api.openweathermap.org\": No address associated with hostname")
                // verifica se a mensagem de erro é a mesma que a do servidor
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                    // adicionado para exibir mensagem de erro caso não tenha conexão com a internet
                }



                else // se não for nenhuma das mensagens acima
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                    // adicionado para exibir mensagem de erro, caso haja algum 
                }
            }
        }
    }

}
