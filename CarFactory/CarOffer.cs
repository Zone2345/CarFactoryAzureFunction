using CarFactory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFactory
{
    public class CarOffer
    {

        [FunctionName("GetCarOffer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "CarManufactory",
            collectionName: "Cars",
            ConnectionStringSetting = "CosmosDbConnectionString")]IEnumerable<Car> getDocument,
            ILogger log)
        {
            string name = req.Query["name"];
            string model = req.Query["model"];
            string color = req.Query["color"];

            log.LogInformation("C# HTTP trigger function processed a request.");
            var manufactory = getDocument.FirstOrDefault(x => x.Manufacturer.ToLower() == name.ToLower());
            if (manufactory == null) return new OkObjectResult("Manufacturer " + name + " u are looking for does not exist!");
            manufactory.Model = getDocument.FirstOrDefault(x => x.Model.ToLower() == model.ToLower()).Model;
            if (manufactory == null) return new OkObjectResult("Model " + model + " u are looking for does not exist!");
            var getOffer = getDocument.FirstOrDefault(x => x.Color.ToLower() == color.ToLower());
            if(getOffer == null) return new OkObjectResult("Color " + color + " u are looking for does not exist!");
            manufactory.Hp = getOffer.Hp;
            manufactory.Engine = getOffer.Engine;
            manufactory.Fuel = getOffer.Fuel;
            manufactory.Color = getOffer.Color;


            return new OkObjectResult(manufactory);
        }
    }
}
