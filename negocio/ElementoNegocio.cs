using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ElementoNegocio
    {

        public List <Elemento> listar ()
        {
            //return new List<Elemento>(); //en realidad deberia incluir todos los elementos para hacer las conexiones
            //                            //a la db
            List <Elemento> lista = new List<Elemento> (); //lista para almacenar los datos de la db
            AccesoDatos datos = new AccesoDatos();  //nace un objeto que tiene un lector, un comando, una conexion.
                                                    //comando / conexion tiene instancia, y ademas, tiene una cadena de configuracion seteada.

            try
            {
                datos.setearConsulta("Select Id, Descripcion From ELEMENTOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion(); //va a cerrar el lector (si existe) y cierra la conexion.
            }
        }
    }
}
