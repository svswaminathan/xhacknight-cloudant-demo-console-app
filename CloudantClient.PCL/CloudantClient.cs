using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using CloudantClient.PCL.Models;

namespace CloudantClient.PCL
{
    public class CloudantClient
    {
        private string cloudantBaseUri = "http://755f10c7-467c-4563-b88c-cf5e115f0c0b-bluemix.cloudant.com";
        private string userName = "755f10c7-467c-4563-b88c-cf5e115f0c0b-bluemix";
        private string password = "3663ff5fc0a6ddff0be8369cf13a145c85a2a8b1760050bf935b5da2fd9a7deb";

        public string CreateDB(string databaseName)
        {
            string responseMessage = string.Empty;
            string createDbApi = cloudantBaseUri + "/" + databaseName;
            HttpClient httpClient = GetCloudantHttpClient();
            StringContent content = new StringContent(string.Empty);
            var response = httpClient.PutAsync(createDbApi, content);
            var statusCode = response.Result.StatusCode;

            switch (statusCode)
            {
                case System.Net.HttpStatusCode.Created:
                    {
                        responseMessage = "Database successfully created";
                        break;
                    }
                case System.Net.HttpStatusCode.Accepted:
                    {
                        responseMessage = "The database has been successfully created on some nodes, but the number of nodes is less than the write quorum";
                        break;
                    }
                case System.Net.HttpStatusCode.Forbidden:
                    {
                        responseMessage = "Invalid database name";
                        break;
                    }
                case System.Net.HttpStatusCode.PreconditionFailed:
                    {
                        responseMessage = "Database aleady exists.";
                        break;
                    }
                default:
                    {
                        responseMessage = "Error in DB creation";
                        break;
                    }
            }
            return responseMessage;
        }

        public string CreateDoc(string databaseName)
        {
            string createDocApi = cloudantBaseUri + "/" + databaseName;
            Hackathon xhacknightHackathon = new Hackathon
            {
                Organiser = "XHackerCO",
                Venue = "",
                Time = new DateTime(2015, 08, 22),
                Sponsors = new List<string>
                {
                    "Xamarin",
                    "IBM"
                }
            };
            HttpClient client = GetCloudantHttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(xhacknightHackathon));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync(createDocApi,content);
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        public string GetAllDbs()
        {
            string getAllDbsApi = "_all_dbs";
            HttpClient restClient = GetCloudantHttpClient();
            var response = restClient.GetAsync(getAllDbsApi);
            var availableDbs = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.SerializeObject(availableDbs);
        }
        
        public string GetDB(string databaseName)
        {
            HttpClient httpClient = GetCloudantHttpClient();
            string getDBApi = cloudantBaseUri + "/" + databaseName;
            var response = httpClient.GetAsync(getDBApi);
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        public string GetAllDocs(string databaseName)
        {
            HttpClient httpClient = new HttpClient();
            string getAllDocsApi = cloudantBaseUri + "/" + databaseName + "/" + "_all_docs";
            var response = httpClient.GetAsync(getAllDocsApi);
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        private HttpClient GetCloudantHttpClient()
        {
            HttpClient restClient = new HttpClient();
            restClient.BaseAddress = new Uri(cloudantBaseUri);
            restClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GetBasicAuthHeaderValue(userName, password));
            return restClient;
        }

        private string GetBasicAuthHeaderValue(string userName, string password)
        {
            string credentials = userName + ":" + password;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }
    }
}
