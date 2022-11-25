
using System;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using TestAppRazorMAUI.Global;

namespace TestAppRazorMAUI.Data
{
    public class FetchSituations
    {
        public async Task<IEnumerable<Situation>> FetchAllSituations()
        {
            var responseContent = default(string);
            var APIadress = Adress.APILocalhost;

			try
			{
				WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
					new Uri("https://mysite.com/mobileauth/Microsoft"),
					new Uri(Adress.CurrentPage));

				string accessToken = authResult?.AccessToken;

				// Do something with the token
			}
			catch (TaskCanceledException e)
			{
				// Use stopped auth
			}

			HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<Situation> test = null;

			

			try
            {
                HttpResponseMessage response = await client.GetAsync(APIadress);
                responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    test = JsonConvert.DeserializeObject<IEnumerable<Situation>>(responseContent);
                }

                return test;
            } catch (Exception ex)
            {

            }

            return null;

        }
    }
}


