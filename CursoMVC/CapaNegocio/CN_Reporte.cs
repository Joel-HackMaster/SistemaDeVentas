using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDatos = new CD_Reporte();

        public Dashboard VerDashboard()
        {
            return objCapaDatos.VerDashboard();
        }

        public List<Reporte> Ventas(string FechaInicio, string FechaFin, string IdTransaccion)
        {
            return objCapaDatos.Ventas(FechaInicio, FechaFin, IdTransaccion);
        }
    }
}
