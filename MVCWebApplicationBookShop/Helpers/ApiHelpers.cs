using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCWebApplicationBookShop.Models;

namespace MVCWebApplicationBookShop.Helpers
{
    public class ApiHelpers
    {
        private static string apiUri = "http://localhost:49981/";


        public static bool GetData(string apiUrl, out object data, string UserName, string Password) 
        {
            data = "";
            using (var client = InitializeClient(UserName, Password))
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("wrong credentials");
                }
                if (response.IsSuccessStatusCode)
                {
                    data = response
                                .Content
                                .ReadAsAsync<object>().Result;
                    return true;
                }
            }
            return false;
        }

        public static HttpResponseMessage PostPut<T>(string url, T obj, string userName, string password, Method method)
        {
            HttpResponseMessage response = null;
            using (var client = InitializeClient(userName, password))
            {
                
                switch (method)
            {
                case Method.PostAsJsonAsync:

                        response = client
                            .PostAsJsonAsync(url, obj)
                            .Result;

                        break;
                case Method.PutAsJsonAsync:

                        response = client
                            .PutAsJsonAsync(url, obj)
                            .Result;

                        break;
                default:
                    break;
            }            
            }
            return response;
        }

        public static HttpResponseMessage DeleteData(string url, int id, string userName, string password)
        {
            HttpResponseMessage response;
            using (var client = InitializeClient(userName, password))
            {
                response = client
                    .DeleteAsync(url + id)
                    .Result;
            }
            return response;
        }

        public static HttpClient InitializeClient(string UserName, string Password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiUri);
            var buffer = Encoding.ASCII.GetBytes(UserName + ":" + Password);
            var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(buffer));
            client.DefaultRequestHeaders.Authorization = authHeader;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}