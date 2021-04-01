using System.Collections.Generic;
using System.Linq;

namespace DevCars.API.Entities
{
    public class Order
    {
        public Order(int id, int idCar, int idCurtumer,decimal price,List<ExtraOrderItem> items)
        {
            this.id = id;
            IdCar = idCar;
            IdCurtumer = idCurtumer;
            TotalCost = items.Sum(i=>i.Price)+ price;
            ExtraItems = items;
        }

        public int id { get; private set; }
        public int IdCar { get; private set; }
        public int IdCurtumer { get; private set; }

        public decimal TotalCost { get; private set; }

        public List<ExtraOrderItem> ExtraItems { get; private set; }
    }

    public class ExtraOrderItem
    {
        public ExtraOrderItem(string descrition, decimal price)
        {
            Descrition = descrition;
            Price = price;
        }

        public int Id { get; private set; }

        public string Descrition { get; private set; }

        public decimal Price { get; private set; }

        public int IdOrder { get; private set; }
    }
}