using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDatos = new CD_Ubicacion();

        public List<Departamento> obtenerDepartamento()
        {
            return objCapaDatos.obtenerDepartamento();
        }

        public List<Provincia> obtenerProvincia(string iddepartamento)
        {
            return objCapaDatos.obtenerProvincia(iddepartamento);
        }

        public List<Distrito> obtenerDistrito(string iddepartamento, string idprovincia)
        {
            return objCapaDatos.obtenerDistrito(iddepartamento, idprovincia);
        }
    }
}
