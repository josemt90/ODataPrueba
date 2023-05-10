using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataPrueba.Models;

namespace ODataPrueba.Controllers
{
    
    public class LibrosController : ODataController
    {
        private readonly BibliotecaODataContext _context;

        public LibrosController(BibliotecaODataContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        // GET: api/Libros/5
        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibros(int id)
        {
            var libros = await _context.Libros.FindAsync(id);

            if (libros == null)
            {
                return NotFound();
            }

            return libros;
        }

        // PUT: api/Libros/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibros(int id, Libros libros)
        {
            if (id != libros.Id)
            {
                return BadRequest();
            }

            _context.Entry(libros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibrosExists(id))
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

        // POST: api/Libros
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Libros>> PostLibros(Libros libros)
        {
            _context.Libros.Add(libros);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LibrosExists(libros.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLibros", new { id = libros.Id }, libros);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Libros>> DeleteLibros(int id)
        {
            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libros);
            await _context.SaveChangesAsync();

            return libros;
        }

        private bool LibrosExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
