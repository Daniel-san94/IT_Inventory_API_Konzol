using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace IT_Inventory_API_Konzol
{
    class Program
    {

        static void Main(string[] qrgs)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44391/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;


                Console.WriteLine("POST");
                TELeltar newTELeltar = new TELeltar();
                newTELeltar.Nev = "surface-5";
                newTELeltar.Hely = "IT";
                newTELeltar.Felhasznalo = "Jancsi";
                newTELeltar.Csoport = "IT";
                newTELeltar.Statusz = "Használva";
                newTELeltar.Tipusok = "Asztali";
                newTELeltar.Gyarto = "MSI";
                newTELeltar.Modell = "GE62";
                newTELeltar.Sorozatszam = "432634";
                newTELeltar.LeltariSzam = "IT-00256";

                response = await client.PostAsJsonAsync("api/TE_Leltar", newTELeltar);
                if(response.IsSuccessStatusCode)
                {
                    Uri teleltarURL = response.Headers.Location;
                    Console.WriteLine(teleltarURL);

                    newTELeltar.Csoport = "Kertészet";

                    //response = await client.PutAsJsonAsync(teleltarURL, newTELeltar);

                    response = await client.DeleteAsync("api/TE_Leltar/");
                }


                Console.WriteLine("GET");
                response = await client.GetAsync("api/TE_Leltar/1");
                if(response.IsSuccessStatusCode)
                {
                    TELeltar tELeltar = await response.Content.ReadAsAsync<TELeltar>();
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", tELeltar.Nev, tELeltar.Hely, tELeltar.Felhasznalo, tELeltar.Csoport,
                        tELeltar.Statusz, tELeltar.Tipusok, tELeltar.Gyarto, tELeltar.Modell, tELeltar.Sorozatszam, tELeltar.LeltariSzam);
                }

                Console.WriteLine("GET");
                response = await client.GetAsync("api/TE_Leltar");
                if (response.IsSuccessStatusCode)
                {
                    List<TELeltar> tELeltar = await response.Content.ReadAsAsync<List<TELeltar>>();
                    for(int i=0; i<tELeltar.Count; i++)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", tELeltar[i].Nev, tELeltar[i].Hely, tELeltar[i].Felhasznalo, tELeltar[i].Csoport,
                        tELeltar[i].Statusz, tELeltar[i].Tipusok, tELeltar[i].Gyarto, tELeltar[i].Modell, tELeltar[i].Sorozatszam, tELeltar[i].LeltariSzam);
                    }
                    
                }
            }
        }



        //private static readonly HttpClient client = new HttpClient();

        //private static async Task ProcessRepositories()
        //{
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        //    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        //    var stringTask = client.GetStringAsync("https://localhost:44391/api/TE_Leltar");

        //    var msg = await stringTask;
        //    Console.Write(msg);
        //}

        //static async Task Main(string[] args)
        //{
        //    await ProcessRepositories();
        //}
       


    }
}
