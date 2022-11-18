
using System;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TestAppRazorMAUI.Data
{
    public class FetchSituations
    {
        //private static string UrlAdress = "https://localhost:7045/api/Situation";
        //Get all suituations (get body -> send request -> gets data -> save data)
        public async Task<IEnumerable<Situation>> FetchAllSituations()
        {

            //using (var client = new HttpClient())
            //{

            //var url = $"https://localhost:7045/api/Situation/";
            var responseContent = default(string);

            HttpClient client = new();
            //client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Situation> test = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync("http://172.16.23.26:7045/api/Situation");
                responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    test = JsonConvert.DeserializeObject<IEnumerable<Situation>>(responseContent);
                    //Situation situation = await response.Content.ReadAsStringAsync<Situation>();
                }

                return test;
            } catch (Exception ex)
            {

            }

            return null;
            
            //client.BaseAddress = newUrl(UrlAdress);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.Timeout = TimeSpan.FromMinutes(1);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = awaitclient.GetAsync("api/Department/1");

            //var response = client.GetAsync(UrlAdress).Result;
            //var resultstring = response.Content.ReadAsStringAsync().Result;
            //var result = JsonConvert.DeserializeObject<Situation>(resultstring);

            //return result;

        }



        //Send the request to the API
  //      public string SendRequest(string url, string requestquery)
  //      {
  //          using (var client = new HttpClient())
  //          {
                
  //          }
  //      }

  //      //Create the body of the request. inc sorts and filter the results
  //      private string CreateRequestQuery(string authenticationkey)
  //      {

  //          return "";
		//}
    }
}


