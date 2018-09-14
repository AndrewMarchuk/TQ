using ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            

            while (true)
            {
                Console.WriteLine("Enter Action:");
                string ID = Console.ReadLine();

                GetRequest(ID).Wait();
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task GetRequest(string ID)
        {
            String uri = "http://localhost:61075/";
            switch (ID)
            {
                //Get Request    
                case "Get":
                    Console.WriteLine("Enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(uri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        //id == 0 means select all records    
                        if (id == 0)
                        {
                            response = await client.GetAsync("api/values");
                            if (response.IsSuccessStatusCode)
                            {
                                BuyerClient[] buyers = await response.Content.ReadAsAsync<BuyerClient[]>();
                                foreach (var buyer in buyers)
                                {
                                    Console.WriteLine("\n{0}\t{1}\t{2}", buyer.Name, buyer.Address, buyer.Age);
                                }
                            }
                        }
                        else
                        {
                            response = await client.GetAsync("api/values/" + id);
                            if (response.IsSuccessStatusCode)
                            {
                                BuyerClient buyer = await response.Content.ReadAsAsync<BuyerClient>();
                                Console.WriteLine("\n{0}\t{1}\t{2}", buyer.Name, buyer.Address, buyer.Age);
                            }
                        }
                    }
                    break;

                //Post Request    
                case "Post":
                    BuyerClient newbuyer = new BuyerClient();
                    Console.WriteLine("Enter Name:");
                    newbuyer.Name = Console.ReadLine();
                    Console.WriteLine("Enter Address:");
                    newbuyer.Address = Console.ReadLine();
                    Console.WriteLine("Enter Age:");
                    newbuyer.Age = Convert.ToInt32(Console.ReadLine());


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(uri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.PostAsJsonAsync("api/values/", newbuyer);

                        if (response.IsSuccessStatusCode)
                        {
                            
                                Console.WriteLine("Report Submitted");
                           
                        }
                    }

                    break;

                //Delete Request    
                case "Delete":
                    Console.WriteLine("Enter id:");
                    int delete = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(uri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.DeleteAsync("api/values/" + delete);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (!result)
                                Console.WriteLine("Buyer Deleted");
                            else
                                Console.WriteLine("An error has occurred");
                        }
                    }
                    break;
            }

        }
    }
}