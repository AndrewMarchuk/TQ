using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleClient
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowBuyer(Buyer buyer)
        {
            Console.WriteLine($"Name: {buyer.Name}\tAddress: " +
                $"{buyer.Address}\tAge: {buyer.Age}");
        }

        static async Task<Uri> CreateBuyerAsync(Buyer buyer)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/values", buyer);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Buyer> GetBuyerAsync(string path)
        {
            Buyer buyer = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                buyer = await response.Content.ReadAsAsync<Buyer>();
            }
            return buyer;
        }

        static async Task<Buyer> UpdateBuyerAsync(Buyer buyer)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/values/{buyer.Id}", buyer);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            buyer = await response.Content.ReadAsAsync<Buyer>();
            return buyer;
        }

        static async Task<HttpStatusCode> DeleteBuyerAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/buyer/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:61075/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
               
                Buyer buyer = new Buyer
                {
                    Name = "Gragor Clegane",
                    Address = "Red Castle",
                    Age = 29
                };

                var url = await CreateBuyerAsync(buyer);
                Console.WriteLine($"Created at {url}");

                // Get the product
                buyer = await GetBuyerAsync(url.PathAndQuery);
                ShowBuyer(buyer);

                // Update the product
                Console.WriteLine("Updating price...");
                buyer.Age = 80;
                await UpdateBuyerAsync(buyer);

                // Get the updated product
                buyer = await GetBuyerAsync(url.PathAndQuery);
                ShowBuyer(buyer);

                // Delete the product
                var statusCode = await DeleteBuyerAsync(buyer.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}