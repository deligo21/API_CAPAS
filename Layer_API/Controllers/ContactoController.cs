using Layer_Modelos;
using Layer_Negocio.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Layer_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _contactoService;

        public ContactoController(IContactoService contactoServ)
        {
            _contactoService = contactoServ;
        }

        // GET: api/<ContactoController>
        [HttpGet]
        public IEnumerable<Contacto> Get()
        {
            return _contactoService.ObtenerTodos();
        }

        // GET api/<ContactoController>/5
        //[HttpGet("{id}")]
        [HttpGet("id/{id}")]
        public Contacto Get(int id)
        {
            return _contactoService.Obtener(id);
        }

        // GET api/<ContactoController>/"Pedro Sanchez"
        [HttpGet("nombre/{nombre}")]
        public Contacto GetNombre(string nombre)
        {
            return _contactoService.ObtenerPorNombre(nombre);
        }

        // POST api/<ContactoController>
        [HttpPost]
        public void Post([FromBody] Contacto value)
        {
            _contactoService.Insertar(value);
        }

        // PUT api/<ContactoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Contacto value)
        {
            if (id == value.IdContacto)
            {
                _contactoService.Actualizar(value);
            }
        }

        // DELETE api/<ContactoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _contactoService.Eliminar(id);
        }
    }
}
