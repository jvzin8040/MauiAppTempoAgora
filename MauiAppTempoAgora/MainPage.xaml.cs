
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

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
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

                        string mapa = $"https://embed.windy.com/embed.html?" +
                            $"type=map&location=coordinates&metricRain=mm&metricTemp=°C" +
                            $"&metricWind=km/h&zoom=7&overlay=wind&product=ecmwf&level=surface" +
                            $"&lat={t.lat.ToString().Replace(",", ".")}&lon={t.lon.ToString().Replace(",", ".")}&message=true";

                        wv_mapa.Source = mapa;
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

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try
            {

                GeolocationRequest request = new GeolocationRequest(
                        GeolocationAccuracy.Medium,
                        TimeSpan.FromSeconds(10)
                    );

                Location? local = await Geolocation.Default.GetLocationAsync(request);

                if (local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude} \n" +
                                        $"Longitude: {local.Longitude}";

                    lbl_coords.Text = local_disp;

                    // pega o nome de cidades atráves das coordenadas
                    GetCidade(local.Latitude, local.Longitude);
                }
                else
                {
                    lbl_coords.Text = "Nenhuma localização";
                }




            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro: Dispositivo sem suporte ", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Erro: Localização Desabilitada ", fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro: Permissão de localização não concedida ", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ocorreu um erro desconhecido: ", ex.Message, "OK");
            }
        }


        private async void GetCidade(double lat, double lon)
        {
            try
            {

                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(lat, lon);
                Placemark? place = places.FirstOrDefault();


                if (place != null)
                {
                    txt_cidade.Text = place.Locality;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Não foi possível obter a cidade: " + ex.Message, "OK");
            }

        }


    }

}
