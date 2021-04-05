using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Entities
{
    public class Car 
    {
        protected Car()
        {

        }
        public Car( string brand, string model, string vinCode, decimal price, int year, string color, DateTime produtionDate)
        {
            this.id = id;
            Brand = brand;
            Model = model;
            VinCode = vinCode;
            Price = price;
            Year = year;
            Color = color;
            ProdutionDate = produtionDate;
            this.status = CarStatusEnum.Available;
            RegisteredAt = DateTime.Now;
        }

        public int id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }

        public string VinCode { get; private set; }

        public decimal Price { get; private set; }

        public int Year { get; private set; }
        public string Color { get; private set; }

        public DateTime ProdutionDate { get; private set; }

        public CarStatusEnum status { get; set; }

        public DateTime RegisteredAt { get; set; }

        public void Update(decimal price, string color)
        {
            Price = price;
            Color = color;
        }
        public void SetAsSuspended()
        {
            status = CarStatusEnum.Suspended;
        }
    }
}
