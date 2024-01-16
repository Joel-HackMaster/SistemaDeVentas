using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDatos = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if(string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creacion de Cuenta";
                string mensaje = "<h3>Su cuenta se creo correctamente</h3><br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje = mensaje.Replace("!clave!",clave);
                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje);
                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertSha256(clave);
                    return objCapaDatos.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar elo correo";
                    return 0;
                }
                
            }
            else
            {
                return 0;
            }
            
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellido del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }

        public bool CambiarClave(int idUsuario, string nuevaClave, out string Mensaje)
        {
            return objCapaDatos.CambiarClave(idUsuario, nuevaClave, out Mensaje);
        }

        public bool ReestablecerClave(int idUsuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDatos.ReestablecerClave(idUsuario, CN_Recursos.ConvertSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Creacion de Cuenta";
                string mensaje = "<h3>Se reestablecio la contraseña</h3><br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje = mensaje.Replace("!clave!", nuevaClave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }

        }

    }
}
