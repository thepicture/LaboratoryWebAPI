using LaboratoryWebAPI.Models.Entities;

namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class ResponseOrderResult
    {
        public ResponseOrderResult(AppliedService appliedService)
        {
            Patient = appliedService.PatientId;
            Services = new ResponseService[]
            {
                new ResponseService
                {
                    Code = appliedService.Service.Id,
                    Result = appliedService.Result.ToString()
                }
            };
        }

        public int Patient { get; set; }
        public ResponseService[] Services { get; set; }
    }
}