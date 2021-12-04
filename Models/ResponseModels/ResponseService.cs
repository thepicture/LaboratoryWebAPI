using LaboratoryWebAPI.Models.Entities;

namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class ResponseService
    {
        public ResponseService(Service service)
        {
            Id = service.Id;
            Name = service.Name;
            Price = service.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}