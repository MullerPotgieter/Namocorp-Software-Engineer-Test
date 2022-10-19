using Namocorp_Contacts_Manager.Data;

namespace Namocorp_Contacts_Manager_Test
{
    
    public class GeographicDataRequesterTest
    {
        
        [Fact]
        public async void testExtractCountryPosition()
        {
            string actualResult;
            
            actualResult = await GeographicDataRequester.extractCountryPosition("");
            Assert.StrictEqual(actualResult, "");

            actualResult = await GeographicDataRequester.extractCountryPosition("a");
            Assert.True(actualResult.Contains("Country not found"));

            actualResult = await GeographicDataRequester.extractCountryPosition("Mexico");

            Assert.True(actualResult.Contains("MX"));
            Assert.True(actualResult.Contains("-102"));
            Assert.True(actualResult.Contains("23"));
        }
    

        [Fact]
        public async void testExtractCountryStates()
        {
            string actualResult;

            actualResult = await GeographicDataRequester.extractCountryStates("");
            Assert.StrictEqual(actualResult, "");
            
            actualResult = await GeographicDataRequester.extractCountryStates("a");
            Assert.True(actualResult.Contains("country not found"));
            
            actualResult = await GeographicDataRequester.extractCountryStates("Mexico");

            Assert.True(actualResult.Contains("Mexico"));
            Assert.True(actualResult.Contains("Tabasco"));
            Assert.True(actualResult.Contains("Zacatecas"));

            Assert.True(actualResult.Contains("GRO"));
            Assert.True(actualResult.Contains("TAM"));
            Assert.True(actualResult.Contains("ZAC"));
            
        }
        
        
        [Fact]
        public async void testExtractStateCities()
        {
            string actualResult;
            
            actualResult = await GeographicDataRequester.extractStateCities("", "");
            Assert.StrictEqual(actualResult, "");

            actualResult = await GeographicDataRequester.extractStateCities("South Africa", "");
            Assert.StrictEqual(actualResult, "");

            actualResult = await GeographicDataRequester.extractStateCities("", "");
            Assert.StrictEqual(actualResult, "");
            
            actualResult = await GeographicDataRequester.extractStateCities("a", "b");
            Assert.True(actualResult.Contains("you seem to be lost"));
            
            actualResult = await GeographicDataRequester.extractStateCities("South Africa", "Gauteng");

            Assert.True(actualResult.Contains("Alberton"));
            Assert.True(actualResult.Contains("Eastleigh"));
            Assert.True(actualResult.Contains("Springs"));
    }

    }
}

