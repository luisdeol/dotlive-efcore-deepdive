namespace EfCoreDeepDive.API.Entities
{
    public class Manufacturer
    {
        public Manufacturer(string manufacturerName, DateTime productionDate, string producerFullAddress)
        {
            ManufacturerName = manufacturerName;
            ProductionDate = productionDate;
            ProducerFullAddress = producerFullAddress;
        }

        public string ManufacturerName { get; set; }
        public DateTime ProductionDate { get; set; }
        public string ProducerFullAddress { get; set; }
    }
}
