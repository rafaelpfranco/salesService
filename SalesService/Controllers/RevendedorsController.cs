using System;
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
using SalesService.Models;

namespace SalesService.Controllers
{
    public class RevendedorsController : ApiController
    {
        private SalesServiceContext db = new SalesServiceContext();

        // GET: api/Revendedors
        public IQueryable<Revendedor> GetRevendedors()
        {
            return db.Revendedors;
        }

        // GET: api/Revendedors/5
        [ResponseType(typeof(Revendedor))]
        public async Task<IHttpActionResult> GetRevendedor(int id)
        {
            Revendedor revendedor = await db.Revendedors.FindAsync(id);
            if (revendedor == null)
            {
                return NotFound();
            }

            return Ok(revendedor);
        }

        // PUT: api/Revendedors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRevendedor(int id, Revendedor revendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != revendedor.Id)
            {
                return BadRequest();
            }

            db.Entry(revendedor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevendedorExists(id))
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

        // POST: api/Revendedors
        [ResponseType(typeof(Revendedor))]
        public async Task<IHttpActionResult> PostRevendedor(Revendedor revendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Revendedors.Add(revendedor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = revendedor.Id }, revendedor);
        }

        // DELETE: api/Revendedors/5
        [ResponseType(typeof(Revendedor))]
        public async Task<IHttpActionResult> DeleteRevendedor(int id)
        {
            Revendedor revendedor = await db.Revendedors.FindAsync(id);
            if (revendedor == null)
            {
                return NotFound();
            }

            db.Revendedors.Remove(revendedor);
            await db.SaveChangesAsync();

            return Ok(revendedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RevendedorExists(int id)
        {
            return db.Revendedors.Count(e => e.Id == id) > 0;
        }
    }
}