using Selenium4Practice.Framework.Grid.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System;

namespace Selenium4Practice.Framework.Grid
{
    public static class SeleniumGridUtilities
    {
        public static IList<string> GetCurrentSeleniumGridSessionIds(string seleniumServerUrl)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(seleniumServerUrl);
                var gridStatus = httpClient.GetFromJsonAsync<SeleniumGridStatus>("/status").Result;
                return gridStatus.Nodes.SelectMany(x => x.Slots).Where(x => x.Session != null).Select(x => x.Session.Id).ToList();
            }
        }

        public static void DeleteSeleniumGridSession(string seleniumServerUrl, string sessionId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(seleniumServerUrl);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-REGISTRATION-SECRET", string.Empty);
                var sessionDeleteResponse = httpClient.DeleteAsync($"/se/grid/node/session/{sessionId}").Result;
                sessionDeleteResponse.EnsureSuccessStatusCode();
            }
        }
    }
}