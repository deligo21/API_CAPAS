using Layer_Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer_Datos.Repositories
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        bool Insertar(TEntityModel model);
        bool Actualizar(TEntityModel model);
        bool Eliminar(int id);
        TEntityModel Obtener(int id);
        List<TEntityModel> ObtenerTodos();
    }
}