﻿namespace MauiAppTempoAgora.Models
{
    public class Tempo
    { 
        public double? lon { get; set; }
        public double? lat { get; set; }
        public double? temp_min { get; set; }
        public double? temp_max { get; set; }
        public int? visibility { get; set; } // adicionado para exibir a visibilidade
        public double? speed { get; set; } // adicionado para exibir a velocidade do vento
        public string? main { get; set; }
        public string? description { get; set; } // adicionado para exibir a descrição do tempo
        public string? sunrise { get; set; }
        public string? sunset { get; set; }

    }

}

/*
public class Clouds
{
    public int all { get; set; }
}



public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
  
    public int pressure { get; set; }
    public int humidity { get; set; }
    public int sea_level { get; set; }
    public int grnd_level { get; set; }
}

public class Rain
{
    [JsonProperty("1h")]
    public double _1h { get; set; }
}

public class Root
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public string @base { get; set; }
    public Main main { get; set; }
   
    public Wind wind { get; set; }
    public Rain rain { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }

}

public class Weather
{
    public int id { get; set; }
    
    public string icon { get; set; }
}

public class Wind
{
  
    public int deg { get; set; }
}

*/