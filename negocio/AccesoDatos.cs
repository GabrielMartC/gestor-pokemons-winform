using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos //Centralizamos la conexion que anteriormente realizamos en PokemonNegocio.
                             //Esto nos sirve para en algun futuro hacer otras acciones sobre la db
    {
        //para hacer una lectura a db...
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector  //para poder leer el this.lector
        {
            get
            {
                return lector;
            }
        }

        public AccesoDatos()
        {
            //cada vez que creemos AccesoDatos, ese objeto se va a crear con una conexion
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta) //metodo que setea el atributo Sqlcommand
                                                    //con una consulta sql
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
                
            //"Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion As Tipo, D.Descripcion As Debilidad From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo and D.Id = P.IdDebilidad";
        }

        public void ejecutarLectura() //este metodo realiza la lectura, y lo guarda en el lector
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void cerrarConexion()
        {
            if (lector != null) //si realize una lectura y tengo el lector...
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
