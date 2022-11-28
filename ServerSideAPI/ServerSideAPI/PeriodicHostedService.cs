using Newtonsoft.Json;
using ServerSideAPI.Model;
using ServerSideAPI.Model.SituationTb;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Xamarin.Essentials;

namespace ServerSideAPI
{
    public class PeriodicHostedService : BackgroundService
    {
		private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(500));
        private static string apiurl = "https://api.trafikinfo.trafikverket.se/v2/data.json";
        private static string authenticationkey = "15c39950faf747ea86962f09867e2114";
        public static string Key = "0";

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

            Key = result.RESPONSE.RESULT[0].INFO.LASTCHANGEID;

			

			if (Key == "0")
            {
				using (var db = new IntraRaddningstjanstDbContext())
				{
					foreach (var item in db.SituationTb)
					{
						db.SituationTb.Remove(item);
					}

					db.SaveChanges();
				}
			}

            foreach (var situation in result.RESPONSE.RESULT[0].Situation)
            {
                foreach (var deviation in situation.Deviation)
                {
                    
                    var Situations = new SituationTb()
                    {
                        Id = deviation.Id,
                        IconId= deviation.IconId,
						Message= deviation.Message,
                        MessageCode= deviation.MessageCode,
                        MessageType= deviation.MessageType,
						CreationTime = deviation.CreationTime
	                };

                    try
                    {
						using (var db = new IntraRaddningstjanstDbContext())
						{
							db.Add(Situations);
							db.SaveChanges();
						}
					} catch(Exception ex)
                    {

                    }

                }

            }

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
            var changeid = 0;

			var request = "<REQUEST>" +
                                  $"<LOGIN authenticationkey = \"{authenticationkey}\" />" +
                                  $"<QUERY objecttype = \"Situation\" schemaversion = \"1.2\" orderby = \"Deviation.CreationTime desc\" changeid=\"{Key}\">" +
                                        "<INCLUDE> Deviation.IconId </INCLUDE>" +
                                        "<INCLUDE> Deviation.Message </INCLUDE>" +
                                        "<INCLUDE> Deviation.MessageCode </INCLUDE>" +
                                        "<INCLUDE> Deviation.MessageType </INCLUDE>" +
                                        "<INCLUDE> Deviation.CreationTime </INCLUDE>" +
										"<INCLUDE> Deviation.Id </INCLUDE>" +
								  "</QUERY> " +
                            "</REQUEST> ";
                return request;
        }
    }
}
