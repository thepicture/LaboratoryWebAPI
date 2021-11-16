using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LaboratoryWebAPI.Models;
using LaboratoryWebAPI.Models.Entities;

namespace LaboratoryWebAPI.Controllers
{
    public class AnalyzersController : ApiController
    {
        private readonly LaboratoryDatabaseEntities db = new LaboratoryDatabaseEntities();

        // GET: api/Analyzers
        public IQueryable<Analyzer> GetAnalyzer()
        {
            return db.Analyzer;
        }

        // GET: api/Analyzers/5
        [ResponseType(typeof(Analyzer))]
        public IHttpActionResult GetAnalyzer(int id)
        {
            Analyzer analyzer = db.Analyzer.Find(id);
            if (analyzer == null)
            {
                return NotFound();
            }

            return Ok(analyzer);
        }

        // PUT: api/Analyzers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnalyzer(int id, Analyzer analyzer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != analyzer.Id)
            {
                return BadRequest();
            }

            db.Entry(analyzer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppliedServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Analyzers
        [ResponseType(typeof(Analyzer))]
        [Route("api/analyzer/{name}")]
        public IHttpActionResult PostAnalyzer(RequestAnalyzer requestAnalyzer, string name)
        {
            if (requestAnalyzer.Services.Count() == 0)
            {
                ModelState.AddModelError("Services", "No services provided to research");
            }
            else
            {
                if (requestAnalyzer.Services
                    .Any(analyzerService => db.Service
                    .FirstOrDefault(service => service.Id == analyzerService.ServiceCode)
                    == null))
                {
                    ModelState.AddModelError("Service", "Some of services do not exist in the database");
                }
            }
            if (!db.Patient.Any(patient => patient.Id == requestAnalyzer.Patient))
            {
                ModelState.AddModelError("PatientId", "No patient was found with the given id");
            }
            if (!db.AppliedService.Select(s => s.FinishedDateTime).Any(date => date < DateTime.Now))
            {
                ModelState.AddModelError("Analyzers", "No avalaible analyzers exist");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (RequestService service in requestAnalyzer.Services)
            {
                AppliedService appliedService = new AppliedService
                {
                    Analyzer = db.Analyzer.ToList().First(a => a.Name.Equals(name)),
                    FinishedDateTime = DateTime.Now + TimeSpan.FromSeconds(30),
                    IsAccepted = false,
                    PatientId = requestAnalyzer.Patient,
                    ServiceId = service.ServiceCode,
                    StatusOfAppliedService = db.StatusOfAppliedService.ToList().First(s => s.Name.StartsWith("Отправлена")),
                    Result = 0,
                    User = db.User.ToList().Last()
                };
                _ = db.AppliedService.Add(appliedService);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/Analyzers/5
        [ResponseType(typeof(Analyzer))]
        public IHttpActionResult DeleteAnalyzer(int id)
        {
            Analyzer analyzer = db.Analyzer.Find(id);
            if (analyzer == null)
            {
                return NotFound();
            }

            db.Analyzer.Remove(analyzer);
            db.SaveChanges();

            return Ok(analyzer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppliedServiceExists(int id)
        {
            return db.AppliedService.Count(e => e.Id == id) > 0;
        }
    }
}