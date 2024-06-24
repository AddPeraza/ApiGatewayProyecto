using ApiGateway.SqlServer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiGateway.SqlServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<Usuario>>> ObtenerNombre(string nombre)
        {
            var usuarios = await _context.Usuarios.Where(u => u.Name.Contains(nombre)).ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ObtenerTodos()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("guid/{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /*
        [HttpPut]
        public async Task<ActionResult<Usuario>> Edit(Usuario model)
        {
            Usuario usuario = await _context.Usuarios.Where(x => x.Id == model.Id).SingleOrDefaultAsync(); //Mejor que FindAsync?

            //Using ModelState.IsValid para verificar la vaildez del modelo utilizado
            if (usuario != null)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.Entry(usuario).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return usuario;
        } 

        [HttpGet]
        public async Task<ActionResult<Usuario>> Delete (int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }*/
    }
}
