using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarDetailsViewModel(int id, string brand, string model, string vinCode, decimal price, string color, DateTime produtionDate)
        {
            this.id = id;
            Brand = brand;
            Model = model;
            VinCode = vinCode;
            Price = price;
            Color = color;
            ProdutionDate = produtionDate;
        }

        public int id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public string VinCode { get; set; }

        public decimal Price { get; set; }

        public string Color { get; set; }

        public DateTime ProdutionDate { get; set; }
    }
}
