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
using LaboratoryWebAPI.Models.Entities;
using LaboratoryWebAPI.Models.ResponseModels;

namespace LaboratoryWebAPI.Controllers
{
    public class LaboratoryNewsController : ApiController
    {
        private LaboratoryDatabaseEntities db = new LaboratoryDatabaseEntities();

        // GET: api/LaboratoryNews
        [Route("api/news/")]
        public IHttpActionResult GetLaboratoryNews()
        {
            return Ok(db.LaboratoryNews.ToList().ConvertAll(l => new ResponseNews(l)));
        }

        [ResponseType(typeof(LaboratoryNews))]
        [Route("api/news/{id}/")]
        public IHttpActionResult GetLaboratoryNews(int id)
        {
            LaboratoryNews laboratoryNews = db.LaboratoryNews.Find(id);
            if (laboratoryNews == null)
            {
                return BadRequest("The given news was not found");
            }

            return Ok(new ResponseNews(laboratoryNews));
        }

        // PUT: api/LaboratoryNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLaboratoryNews(int id, LaboratoryNews laboratoryNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != laboratoryNews.Id)
            {
                return BadRequest();
            }

            db.Entry(laboratoryNews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaboratoryNewsExists(id))
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

        // POST: api/LaboratoryNews
        [ResponseType(typeof(LaboratoryNews))]
        public IHttpActionResult PostLaboratoryNews(LaboratoryNews laboratoryNews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LaboratoryNews.Add(laboratoryNews);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = laboratoryNews.Id }, laboratoryNews);
        }

        // DELETE: api/LaboratoryNews/5
        [ResponseType(typeof(LaboratoryNews))]
        public IHttpActionResult DeleteLaboratoryNews(int id)
        {
            LaboratoryNews laboratoryNews = db.LaboratoryNews.Find(id);
            if (laboratoryNews == null)
            {
                return NotFound();
            }

            db.LaboratoryNews.Remove(laboratoryNews);
            db.SaveChanges();

            return Ok(laboratoryNews);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LaboratoryNewsExists(int id)
        {
            return db.LaboratoryNews.Count(e => e.Id == id) > 0;
        }
    }
}