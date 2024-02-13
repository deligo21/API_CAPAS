using Layer_Datos.DataContext;
using Layer_Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Datos.Repositories
{
    public class ContactoRepository : IGenericRepository<Contacto>
    {
        private readonly CrudCapasContext _dbcontext;
        public ContactoRepository(CrudCapasContext context)
        {
            this._dbcontext = context;
        }
        public bool Actualizar(Contacto model)
        {
            _dbcontext.Contactos.Update(model);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool Eliminar(int id)
        {
            Contacto modelo = _dbcontext.Contactos.First(c => c.IdContacto == id);
            _dbcontext.Contactos.Remove(modelo);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool Insertar(Contacto model)
        {
            _dbcontext.Contactos.Add(model);
            _dbcontext.SaveChanges();
            return true;
        }

        public Contacto Obtener(int id)
        {
            return _dbcontext.Contactos.Find(id);
        }

        public List<Contacto> ObtenerTodos()
        {
            return _dbcontext.Contactos.ToList();
        }
    }
}