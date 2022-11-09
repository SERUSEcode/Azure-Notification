
using System;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace TestAppRazorMAUI.Data
{
    public class FetchSituations
    {
        private static string apiurl = "https://api.trafikinfo.trafikverket.se/v2/data.json";
        private static string authenticationkey = "15c39950faf747ea86962f09867e2114";

        public Rootobject FetchAllSituations()
        {
            string requestquery = CreateRequestQuery(authenticationkey);
            string responsestring = SendRequest(apiurl, requestquery);
            var result = JsonConvert.DeserializeObject<Rootobject>(responsestring);

            
            return result;
        }


        public string SendRequest(string url, string requestquery)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(1);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync(url, new StringContent(requestquery, Encoding.UTF8, "application/xml")).Result;
                var resultstring = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Server responded with status code " + response.StatusCode);
                }
                return resultstring;
            }
        }

        private string CreateRequestQuery(string authenticationkey)
        {
            var request = "<REQUEST>" +
                              $"<LOGIN authenticationkey = \"{authenticationkey}\" />" +
                               "<QUERY objecttype = \"Situation\" schemaversion = \"1.2\" >" +
                                    "<INCLUDE> Deviation.Header </INCLUDE>" +
                                    "<INCLUDE> Deviation.IconId </INCLUDE>" +
                                    "<INCLUDE> Deviation.Message </INCLUDE>" +
                                    "<INCLUDE> Deviation.MessageCode </INCLUDE>" +
                                    "<INCLUDE> Deviation.MessageType </INCLUDE>" +
                              "</QUERY> " +
                        "</REQUEST> ";
            return request;
        }

    }
    
}


