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

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada"; // adicionado para exibir mensagem de cidade não encontrada
                    }
                }
                else
                {
                    lbl_res.Text = "Digite o nome de uma cidade";
                }

            } // fecha try
            catch (Exception ex)
            {
                if (ex.Message == "No such host is known. (api.openweathermap.org:443)")
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                }
                else if (ex.Message == "Connection failure")
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                }
                else if (ex.Message == "Unable to resolve host \"api.openweathermap.org\": No address associated with hostname")
                {
                    await DisplayAlert("Ops", "Sem conexão com a internet", "OK");
                }
                else
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                }
            }
        }
    }

}
