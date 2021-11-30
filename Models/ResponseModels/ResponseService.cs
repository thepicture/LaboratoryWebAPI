using LaboratoryWebAPI.Models.Entities;

namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class ResponseService
    {
        public ResponseService(Service service)
        {
            Name = service.Name;
            Price = service.Price;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}