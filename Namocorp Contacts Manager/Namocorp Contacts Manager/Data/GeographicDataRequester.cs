using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Namocorp_Contacts_Manager.Data
{
    public static class GeographicDataRequester
    {
        private static readonly HttpClient client = new HttpClient();

        static string baseUrl = "https://countriesnow.space/api/v0.1/countries/";

        public static async Task<string> extractCountryPosition(string countryName)
        {
            try
            {
                string iso2, longitude, latitude, positionData;

                if (String.IsNullOrEmpty(countryName))
                    return "";

                var values = new Dictionary<string, string>
                {
                    { "country", countryName }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(baseUrl + "positions", content);

                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString.Contains("\"error\":true"))
                    return "Country's states data could not be returned.Exeption: " + responseString;
                else
                {
                    JObject obj = JObject.Parse(responseString);
                    JToken jToken = obj["data"];

                    iso2 = jToken["iso2"].ToString();
                    longitude = jToken["long"].ToString();
                    latitude = jToken["lat"].ToString();

                    positionData = "ISO 2		: " + iso2 + "\n" +
                                    "Longitude	: " + longitude + "\n" +
                                    "Latitude	: " + latitude;

                    return positionData;
                }
            }
            catch (Exception ex)
            {
                return "Country's position data could not be returned. Exception: " + ex.Message;
            }
        }

        public static async Task<string> extractCountryStates(string countryName)
        {
            try
            {
                string result = "Name:\t\t \t\tCode:\n";

                if (String.IsNullOrEmpty(countryName))
                    return "";
               

                var values = new Dictionary<string, string>
                {
                    { "country", countryName }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(baseUrl + "states", content);

                var responseString = await response.Content.ReadAsStringAsync();

                
                if (responseString.Contains("\"error\":true"))
                    return "Country's states data could not be returned. Exeption: " + responseString;
                else
                {
                    responseString = responseString.Substring(responseString.IndexOf("["), responseString.IndexOf("]") - responseString.IndexOf("[") + 1);

                    dynamic stateInfo = JsonConvert.DeserializeObject(responseString);

                    foreach (var state in stateInfo)
                        result += state.name + "\t\t \t" + state.state_code + "\n";

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "Country's states data could not be returned. Exception: " + ex.Message;
            }
        }

        public static async Task<string> extractStateCities(string countryName, string stateName)
        {
            try
            {
                string result = "Cities in " + countryName + "'s state of " + stateName + ":\n";

                if (String.IsNullOrEmpty(countryName) || String.IsNullOrEmpty(stateName))
                    return "";

                var values = new Dictionary<string, string>
                {
                    { "country", countryName },
                    { "state", stateName }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(baseUrl + "state/cities", content);

                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString.Contains("\"error\":true"))
                    return "Country states data could not be returned. Exception: " + responseString;
                else
                {
                    responseString = responseString.Substring(responseString.IndexOf("["), responseString.IndexOf("]") - responseString.IndexOf("[") + 1);

                    dynamic stateInfo = JsonConvert.DeserializeObject(responseString);

                    foreach (var state in stateInfo)
                        result += state.Value + "\n";

                    return result;
                }
            }
            catch (Exception ex)
            {
                return "State city data could not be returned. Exception: " + ex.Message;
            }
        }

    }
}
