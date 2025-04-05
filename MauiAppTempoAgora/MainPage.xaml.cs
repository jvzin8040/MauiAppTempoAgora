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
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat}\n" +
                            $"Longitude: {t.lon}\n" +
                            $"Nascer do Sol: {t.sunrise}\n" +
                            $"Pôr do Sol: {t.sunset}\n" +
                            $"Temp Máx: {t.temp_max}°C\n" +
                            $"Temp Mín: {t.temp_min}°C\n" +
                            $"Velocidade do Vento: {t.speed} m/s\n" +
                            $"Visibilidade: {t.visibility} m\n" +
                            $"Descrição: {t.description}\n";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Cidade não encontrada";
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
                else
                {
                    await DisplayAlert("Ops", ex.Message, "OK");
                }
            }
        }
    }

}
