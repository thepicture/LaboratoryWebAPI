using LaboratoryWebAPI.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LaboratoryWebAPI.Models.ResponseModels
{
    public class ResponseOrderResult
    {
        public ResponseOrderResult(List<AppliedService> appliedServicesList)
        {
            Patient = appliedServicesList.First().PatientId;
            Services = appliedServicesList.Select(s =>
            {
                return new ResponseService
                {
                    Code = s.Id,
                    Result = s.Result.ToString()
                };
            }).ToArray();
        }

        public int Patient { get; set; }
        public ResponseService[] Services { get; set; }
    }
}