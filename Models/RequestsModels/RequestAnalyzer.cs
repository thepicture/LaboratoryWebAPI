namespace LaboratoryWebAPI.Models
{
    public class RequestAnalyzer
    {
        public int PatientId { get; set; }
        public RequestService[] Services { get; set; }
    }
}