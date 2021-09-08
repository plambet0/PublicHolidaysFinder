using PublicHolidaysFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublicHolidaysFinder.Services
{
    public class HolidaysApiService : IHolidaysApiService
    {
        private readonly HttpClient client;

        public  HolidaysApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicHolidaysApi");
        }

        public async Task<List<HolidayModel>> GetHolidays(string countryCode, int year)
        {
            //The first line is building the Url of Nager.Date API and using the year and countryCode parameters

            var url = string.Format("/api/v2/PublicHolidays/{0}/{1}", year, countryCode);

            var result = new List<HolidayModel>();

            /*Next, we are making an API call using the GetAsync method that sends a GET request to the specified Uri as an asynchronous operation.
             * The method returns System.Net.Http.HttpResponseMessage object that represents an HTTP response message including the status code and data.*/

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                //Next, we are calling ReadAsStringAsync method that serializes the HTTP content to a string

                var stringResponse = await response.Content.ReadAsStringAsync();

                //Finally, we are using JsonSerializer to Deserialize the JSON response string into a List of HolidayModel objects.

                result = JsonSerializer.Deserialize<List<HolidayModel>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
