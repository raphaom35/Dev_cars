using DevCars.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCars.API.Presistence
{
    public class DevCarsDbContext
    {
        public DevCarsDbContext()
        {
            Cars = new List<Car>
            {
                new Car(1,"123BCD","Honda","Civic",2021,100000,"Cinza",new DateTime(2021,1,1)),
                new Car(2,"444BCD","Toyota","Corola",2021,95000,"Azul",new DateTime(2021,1,1)),
                new Car(3,"999BCD","Chevrolet","Oniz",2021,85000,"Azul",new DateTime(2021,1,1))
            };
            Custumers = new List<Custumer>
            {
                new Custumer(1,"Raphael","211323123",new DateTime(1990,1,1)),
                 new Custumer(1,"Renato","989898989",new DateTime(1990,1,1)),
            };
        }
        public List<Car> Cars { get; set; }

        public List<Custumer> Custumers { get; set; }
    }
}
