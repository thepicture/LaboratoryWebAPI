namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class RequestLoginPatient
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}