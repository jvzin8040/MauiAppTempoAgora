using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;


namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;
            string chave = "1e1b1dddc563fb44f7c8c048732b250c";
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);
           
                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();

                        var rascunho = JObject.Parse(json);

                        DateTime time = new();
                        DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                        t = new()

                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"], // adicionado para exibir a descrição do tempo
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"], // adicionado para exibir a velocidade do vento
                            visibility = (int)rascunho["visibility"], // adicionado para exibir a visibilidade
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                        }; // Fecha obj do Tempo.

                    } // Fecha if se o status  servidor for sucesso
            } // fecha laço using

            return t;
        }
    }
}
