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

        // GET: api/news
        [Route("api/news/")]
        [ResponseType(typeof(List<ResponseNews>))]
        public IHttpActionResult GetLaboratoryNews()
        {
            return Ok
                (
                    new
                    {
                        news = db.LaboratoryNews.ToList().ConvertAll(l => new ResponseNews(l))
                    }
                );
        }

        // GET : api/news/1
        [ResponseType(typeof(ResponseNews))]
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