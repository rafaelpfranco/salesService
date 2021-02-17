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
    public class ProdutoesController : ApiController
    {
        private SalesServiceContext db = new SalesServiceContext();

        // GET: api/Produtoes
        public IQueryable<ProdutoDto> GetProdutoes()
        {
            var produtoes = from p in db.Produtoes
                        select new ProdutoDto()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            RevendedorName = p.Revendedor.Name
                        };

            return produtoes;
        }

        // GET: api/Produtoes/5
        [ResponseType(typeof(ProdutoDetailDto))]
        public async Task<IHttpActionResult> GetProduto(int id)
        {
            var produto = await db.Produtoes.Include(p => p.Revendedor).Select(p =>
            new ProdutoDetailDto()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category,
                Description = p.Description,
                RevendedorName = p.Revendedor.Name
            }).SingleOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduto(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.Id)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        // POST: api/Produtoes
        [ResponseType(typeof(ProdutoDto))]
        public async Task<IHttpActionResult> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produtoes.Add(produto);
            await db.SaveChangesAsync();

            db.Entry(produto).Reference(x => x.Revendedor).Load();

            var dto = new ProdutoDto()
            {
                Id = produto.Id,
                Name = produto.Name,
                RevendedorName = produto.Revendedor.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtoes/5
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> DeleteProduto(int id)
        {
            Produto produto = await db.Produtoes.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtoes.Remove(produto);
            await db.SaveChangesAsync();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produtoes.Count(e => e.Id == id) > 0;
        }
    }
}