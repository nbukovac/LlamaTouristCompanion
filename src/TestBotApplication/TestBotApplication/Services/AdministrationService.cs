using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using TestBotApplication.Models;
using static TestBotApplication.Models.GoogleMapLocationModel;

namespace TestBotApplication.Services
{
    public class AdministrationService
    {
        public List<AdministrationEventClass> GetPossibleLocations(Location location)
        {

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                client.BaseAddress = new Uri("http://apartmanko.azurewebsites.net");
                var latitude = location.lat.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                var longitude = location.lng.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture);
                HttpResponseMessage googleApiResponse = client.GetAsync("/api/botcache/apartments?latitude=" + latitude + "&longitude=" + longitude).Result;
                googleApiResponse.EnsureSuccessStatusCode();
                var tmp = googleApiResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<AdministrationEventClass>>(tmp);
                return result;
            }
        }

        public BotCacheModel GetResponeseToQuery(string  tokens, string apartmantId)
        {

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                client.BaseAddress = new Uri("http://apartmanko.azurewebsites.net");
                HttpResponseMessage googleApiResponse = client.GetAsync("api/botcache/answers?tokens="+  tokens +"&apartmentId=" + apartmantId).Result;
                googleApiResponse.EnsureSuccessStatusCode();
                var tmp = googleApiResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<BotCacheModel>>(tmp).FirstOrDefault();
                return result;
            }
        }
    }
}