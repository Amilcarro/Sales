﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Sales.Common.Models;
using Sales.Domain.Models;

namespace Sales.API.Controllers
{
    public class PartsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Parts
        public IQueryable<Part> GetParts()
        {
            return db.Parts;
        }

        // GET: api/Parts/5
        [ResponseType(typeof(Part))]
        public async Task<IHttpActionResult> GetPart(string id)
        {
            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        // PUT: api/Parts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPart(string id, Part part)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != part.Id)
            {
                return BadRequest();
            }

            db.Entry(part).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartExists(id))
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

        // POST: api/Parts
        [ResponseType(typeof(Part))]
        public async Task<IHttpActionResult> PostPart(Part part)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parts.Add(part);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PartExists(part.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = part.Id }, part);
        }

        // DELETE: api/Parts/5
        [ResponseType(typeof(Part))]
        public async Task<IHttpActionResult> DeletePart(string id)
        {
            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }

            db.Parts.Remove(part);
            await db.SaveChangesAsync();

            return Ok(part);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartExists(string id)
        {
            return db.Parts.Count(e => e.Id == id) > 0;
        }
    }
}