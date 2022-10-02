using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Manufacturer {get; set; }
        public string Model {get; set;}
        public string Color{get; set; }
        public int Hp    {get; set; }
        public int Engine   {get; set; }
        public string Fuel  {get; set; }
    }
}
