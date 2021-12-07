using LaboratoryWebAPI.Models.Entities;
using LaboratoryWebAPI.Models.RequestModels;
using LaboratoryWebAPI.Models.RequestResponseModels;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LaboratoryWebAPI.Controllers
{
    public class PatientsController : ApiController
    {
        private LaboratoryDatabaseEntities db = new LaboratoryDatabaseEntities();

        // POST: api/patient/login
        [HttpPost]
        [Route("api/patient/login")]
        [ResponseType(typeof(RequestResponsePatient))]
        public IHttpActionResult GetPatientDataIfLoginIsSuccessful(
            RequestLoginPatient requestPatient)
        {
            Patient dbPatient = db.Patient
                .FirstOrDefault(p => p.Login == requestPatient.Login
                                     && p.Password == requestPatient.Password);
            if (dbPatient != null)
            {
                return Ok(new RequestResponsePatient(dbPatient));
            }
            else
            {
                return BadRequest("Incorrect login " +
                    "and/or password of the patient");
            }
        }

        // POST: api/patient/register
        [HttpPost]
        [Route("api/patient/register")]
        [ResponseType(typeof(RequestResponsePatient))]
        public IHttpActionResult RegisterPatient(
            RequestResponsePatient requestPatient)
        {
            Patient dbPatient = db.Patient
                .FirstOrDefault
                (
                    p => p.Login.ToLower() == requestPatient
                    .LoginAndPassword.Login.ToLower()
                );
            if (dbPatient == null)
            {
                Patient savedPatient = db.Patient.Add(new Patient
                {
                    BirthDate = requestPatient.BirthDate,
                    Email = requestPatient.Email,
                    FullName = requestPatient.FullName,
                    Login = requestPatient.LoginAndPassword.Login,
                    Password = requestPatient.LoginAndPassword.Password,
                    Phone = requestPatient.PhoneNumber,
                    InsurancePolicyNumber = requestPatient.InsuranceNumber,
                    PassportNumber = requestPatient.PassNum,
                    PassportSeries = requestPatient.PassSeries
                });
                db.SaveChanges();
                return Ok(new RequestResponsePatient(savedPatient));
            }
            else
            {
                return BadRequest("The patient with given login and password " +
                    "already exists in the database");
            }
        }

        // PUT: api/patient/profile
        [HttpPut]
        [Route("api/patient/profile")]
        [ResponseType(typeof(RequestResponsePatient))]
        public IHttpActionResult UpdatePatient(
            [FromBody] RequestResponsePatient requestPatient)
        {
            Patient dbPatient = db.Patient
                .FirstOrDefault(p => p.Login == requestPatient.LoginAndPassword.Login
            && p.Password == requestPatient.LoginAndPassword.Password);
            if (dbPatient != null)
            {
                if (string.IsNullOrWhiteSpace(requestPatient.PhoneNumber))
                {
                    ModelState.AddModelError(nameof(requestPatient.PhoneNumber),
                        "PhoneNumber is invalid");
                }
                if (string.IsNullOrWhiteSpace(requestPatient.Email))
                {
                    ModelState.AddModelError(nameof(requestPatient.Email),
                        "Email is invalid");
                }
                if (requestPatient.LoginAndPassword.NewPassword
                    != requestPatient.LoginAndPassword.Password
                    || requestPatient.LoginAndPassword
                    .NewPassword.Length < 5)
                {
                    ModelState.AddModelError(
                        nameof(requestPatient.LoginAndPassword.Password),
                        "New password is same as the old one or is too short");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                dbPatient.Phone = requestPatient.PhoneNumber;
                dbPatient.Email = requestPatient.Email;
                dbPatient.Password = requestPatient.LoginAndPassword.NewPassword;
                db.SaveChanges();
                return Ok(new RequestResponsePatient(dbPatient));
            }
            else
            {
                return BadRequest("Can't update profile: " +
                    "incorrect login and/or password of the patient");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}