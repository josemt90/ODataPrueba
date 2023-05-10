using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataPrueba;
using ODataPrueba.Models;

namespace ODataPrueba.Controllers
{
    
    public class AutoresController : ODataController
    {
        private readonly BibliotecaODataContext _context;

        public AutoresController(BibliotecaODataContext context)
        {
            _context = context;
        }

        // GET: api/Autores
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autores>>> GetAutores()
        {
            return await _context.Autores.Include(l=>l.Libros).ToListAsync();
        }

        // GET: api/Autores/5
        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<Autores>> GetAutores(int id)
        {
            var autores = await _context.Autores.Include(l => l.Libros).Where(l=>l.Id==id).SingleOrDefaultAsync();

            if (autores == null)
            {
                return NotFound();
            }

            return autores;
        }

        // PUT: api/Autores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutores(int id, Autores autores)
        {
            if (id != autores.Id)
            {
                return BadRequest();
            }

            _context.Entry(autores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Autores>> PostAutores(Autores autores)
        {
            _context.Autores.Add(autores);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AutoresExists(autores.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAutores", new { id = autores.Id }, autores);
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autores>> DeleteAutores(int id)
        {
            var autores = await _context.Autores.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autores);
            await _context.SaveChangesAsync();

            return autores;
        }

        private bool AutoresExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
