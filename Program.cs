using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ConsumeWebApi_1
{
   class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        static void Main(string[] args)
        {
             ProcessRepositories("01606302018", "58092582018").Wait();
        }
    
        private static async Task ProcessRepositories(string strDt, string strGmci)
        {
           
            string URI;
            URI = "http://gmcieletronica.btp.com.br:8084/Avaria?Dt=" + strDt + "&Gmci=" + strGmci + "";
            
            //var stringTask = client.GetStringAsync(URI);

            var streamTask = client.GetStreamAsync(URI);
            var serializer = new DataContractJsonSerializer(typeof(List<Avaria>));
            var repositories = serializer.ReadObject(await streamTask) as List<Avaria>;

            // var msg = await stringTask;
            //Console.Write(msg);"";

            string msg = "";            

            foreach (var repo in repositories) 
            {
                msg = repo.CodLocal + " "  + repo.CodTipo + " " + repo.DescLocal + " " + repo.DescTipo + " ";
                Console.WriteLine(msg);
            }   
              
        }


    }
}
