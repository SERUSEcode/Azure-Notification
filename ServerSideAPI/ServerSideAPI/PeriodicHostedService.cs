using Newtonsoft.Json;
using ServerSideAPI.Model;
using System.Net.Http.Headers;
using System.Text;

namespace ServerSideAPI
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(10000));
        private Rootobject? result;
        private static string apiurl = "https://api.trafikinfo.trafikverket.se/v2/data.json";
        private static string authenticationkey = "15c39950faf747ea86962f09867e2114";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync();
            }
        }

        private async Task DoWorkAsync()
        {
            string requestquery = CreateRequestQuery(authenticationkey);
            string responsestring = SendRequest(apiurl, requestquery);

            var result = JsonConvert.DeserializeObject<Rootobject>(responsestring);

            

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

        //Create the body of the request. inc sorts and filter the results
        private string CreateRequestQuery(string authenticationkey)
        {
            var changeid = "0";

                var request = "<REQUEST>" +
                                  $"<LOGIN authenticationkey = \"{authenticationkey}\" />" +
                                  $"<QUERY objecttype = \"Situation\" schemaversion = \"1.2\" orderby = \"Deviation.CreationTime desc\" changeid=\"{changeid}\">" +
                                        "<INCLUDE> Deviation.IconId </INCLUDE>" +
                                        "<INCLUDE> Deviation.Message </INCLUDE>" +
                                        "<INCLUDE> Deviation.MessageCode </INCLUDE>" +
                                        "<INCLUDE> Deviation.MessageType </INCLUDE>" +
                                        "<INCLUDE> Deviation.CreationTime </INCLUDE>" +
                                  "</QUERY> " +
                            "</REQUEST> ";
                return request;
        }
    }
}
