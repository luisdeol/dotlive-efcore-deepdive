namespace EfCoreDeepDive.API.Entities
{
        public class Manufacturer
        {
            public Manufacturer(string name, DateTime productionDate, string productFullAddress)
            {
                Name = name;
                ProductionDate = productionDate;
                ProductFullAddress = productFullAddress;
            }

            public string Name { get; set; }
            public DateTime ProductionDate { get; set; }
            public string ProductFullAddress { get; set; }
        }
}
