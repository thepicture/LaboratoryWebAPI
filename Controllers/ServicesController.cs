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
    public class ServicesController : ApiController
    {
        private LaboratoryDatabaseEntities db = new LaboratoryDatabaseEntities();

        // GET: api/service
        [Route("api/service")]
        [ResponseType(typeof(List<ResponseService>))]
        public IHttpActionResult GetService()
        {
            return Ok
                (
                    new
                    {
                        Services = db.Service.ToList().ConvertAll(s => new ResponseService(s))
                    }
                );
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