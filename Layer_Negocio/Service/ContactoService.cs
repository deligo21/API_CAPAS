using Layer_Datos.Repositories;
using Layer_Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Negocio.Service
{
    public class ContactoService : IContactoService
    {
        private readonly IGenericRepository<Contacto> _contactRepo;
        public ContactoService(IGenericRepository<Contacto> _contactRepo)
        {
            this._contactRepo = _contactRepo;
        }

        public bool Actualizar(Contacto model)
        {
            return _contactRepo.Actualizar(model);
        }

        public bool Eliminar(int id)
        {
            return _contactRepo.Eliminar(id);
        }

        public bool Insertar(Contacto model)
        {
            return _contactRepo.Insertar(model);
        }

        public Contacto Obtener(int id)
        {
            return _contactRepo.Obtener(id);
        }

        public Contacto ObtenerPorNombre(string nombreContacto)
        {
            IEnumerable<Contacto> queryContactoSQL = _contactRepo.ObtenerTodos();
            Contacto contacto = queryContactoSQL.Where(c => c.Nombre == nombreContacto).FirstOrDefault();
            return contacto;
        }


        List<Contacto> IContactoService.ObtenerTodos()
        {
            return _contactRepo.ObtenerTodos();
        }
    }
}
