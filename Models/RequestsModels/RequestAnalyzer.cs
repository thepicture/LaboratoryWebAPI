namespace LaboratoryWebAPI.Models
{
    public class RequestAnalyzer
    {
        public int Patient { get; set; }
        public RequestService[] Services { get; set; }
    }
}