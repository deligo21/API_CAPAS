using Layer_Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Negocio.Service
{
    public interface IContactoService
    {
        bool Insertar(Contacto model);
        bool Actualizar(Contacto model);
        bool Eliminar(int id);
        Contacto Obtener(int id);
        List<Contacto> ObtenerTodos();

        Contacto ObtenerPorNombre(string nombreContacto);
    }
}