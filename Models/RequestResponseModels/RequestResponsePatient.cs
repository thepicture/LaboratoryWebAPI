using LaboratoryWebAPI.Models.Entities;
using LaboratoryWebAPI.Models.RequestModels;
using System;

namespace LaboratoryWebAPI.Models.RequestResponseModels
{
    public class RequestResponsePatient
    {
        public RequestResponsePatient()
        {
        }

        public RequestResponsePatient(Patient patient)
        {
            FullName = patient.FullName;
            PassNum = patient.PassportNumber;
            PassSeries = patient.PassportSeries;
            PhoneNumber = patient.Phone;
            Email = patient.Email;
            BirthDate = patient.BirthDate;
            InsuranceNumber = patient.InsurancePolicyNumber;
            LoginAndPassword = new RequestLoginPatient
            {
                Login = patient.Login,
                Password = patient.Password
            };
        }

        public string FullName { get; set; }
        public string PassNum { get; set; }
        public string PassSeries { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string InsuranceNumber { get; set; }
        public RequestLoginPatient LoginAndPassword { get; set; }
    }
}