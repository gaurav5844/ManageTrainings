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
using TrainingManager.Models;

namespace TrainingManager.Controllers
{
    public class ManageTrainingsController : ApiController
    {
        private DBTraining db = new DBTraining();
               
        // GET: api/ManageTrainings
        public IQueryable<ManageTraining> GetAllTrainings()
        {
            return db.ManageTrainings;
        }

        // GET: api/ManageTrainings/5
        [ResponseType(typeof(ManageTraining))]
        public IHttpActionResult GetTraining(int id)
        {
            ManageTraining manageTraining = db.ManageTrainings.Find(id);
            if (manageTraining == null)
            {
                return NotFound();
            }

            return Ok(manageTraining);
        }       

        // POST: api/ManageTrainings
        [ResponseType(typeof(ManageTraining))]
        public IHttpActionResult CreateTraining(ManageTraining manageTraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ManageTrainings.Add(manageTraining);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainingExists(manageTraining.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = manageTraining.Id }, manageTraining);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainingExists(int id)
        {
            return db.ManageTrainings.Count(e => e.Id == id) > 0;
        }
    }
}