using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int idCar, int idCustumer, decimal totalcost, List<string> extraItems)
        {
            IdCar = idCar;
            IdCustumer = idCustumer;
            Totalcost = totalcost;
            ExtraItems = extraItems;
        }

        public int IdCar { get; set; }
        public int IdCustumer { get; set; }
        public decimal Totalcost { get; set; }
        public List<string> ExtraItems { get; set; }
    }
}
