using System.Diagnostics;
using MauiTempoAgora.Models;
using MauiTempoAgora.Servies;

namespace MauiTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked_Previsao(object sender, EventArgs e)
        {

        }

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try 
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text)) 
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if(t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do sol: {t.sunrise} \n" +
                                         $"Por so sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n";
                        lbl_res.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?" +
                                      $"type=map&locatiopn=coordinates&metricRain=mm&metricTemp=°C" +
                                      $"&metricWind=km/h&zoom=5&overlay=wind&product=ecmwf&level=surface" +
                                      $"&lat={t.lat.ToString().Replace(",", ".")}&Lon=" +
                                      $"{t.lon.ToString().Replace(", ", ".")}";
                        
                        wv_mapa.Sourse = mapa;

                        Debug.WriteLine(mapa);
                    }
                    else 
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }// fecha if t=null
                }
                else 
                {
                    lbl_res.Text = "preencha a cidade.";
                }//Fecha if string is null or empty
            }catch (Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message);
            } 
        }
    }

}
