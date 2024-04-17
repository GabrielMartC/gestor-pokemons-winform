using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //lo importamos para establecer una conexion a bases de datos


namespace ejemplos_ado_dotnet
{
    class PokemonNegocio  //aca creamos los metodos de accesos a datos
    {
        /*Creamos un metodo public para que pueda ser accedido desde el exterior. Para que lea registros de la DB creamos
        una lista. 
         */
        public List<Pokemon> listar() 
        {
            List<Pokemon> lista = new List<Pokemon>();

            SqlConnection conexion = new SqlConnection(); //Nos permite extrablecer una conexion a la DB
            SqlCommand comando = new SqlCommand(); //Nos permite realizar acciones en la DB
            SqlDataReader lector; //Nos permite obtener en un vector un set de datos de la DB. No hace falta el uso del constructor.

            try //para poder manejar excepciones. En try toda la funcionalidad que puede fallar. 
            {
                //- - - CONFIGURACIONES - - - 
                //conexion.ConnectionString = "server=DESKTOP-V8QQ7P3\\SQLEXPRESS"; //forma 1 de la Cadena de Conexion
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true"; //forma 2  de la Cadena de Conexion
                                   /*a donde nos vamos a conectar; a que base de datos; "como" me voy a conectar */
                comando.CommandType = System.Data.CommandType.Text; /*hay tres tipos de comandos(realizar la accion):
                                                                     tipo texto, procedimiento almacenado, enlace directo 
                                                                     con la tabla*/
                //CONSULTA SOLO CON 1 TABLA
                //comando.CommandText = "Select Numero, Nombre, Descripcion, UrlImagen from POKEMONS"; //consulta que enviamos a la DB

                //CONSULTA CON 2 TABLAS
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion As Tipo, D.Descripcion As Debilidad From POKEMONS P, ELEMENTOS E, ELEMENTOS D Where E.Id = P.IdTipo and D.Id = P.IdDebilidad"; 

                comando.Connection = conexion; //va a ejecutar el comando de la linea anterior

                conexion.Open(); // abrir la conexion
                lector = comando.ExecuteReader(); //realizo la lectura

                while (lector.Read()) //si hay un registro a continuacion devuelve true, y apuntando a c/u los registros de la DB
                {
                    Pokemon aux = new Pokemon();
                    /**/
                    //aux.Numero = lector.GetInt32(0); //1ra forma

                    /* (casteo explicito segun el tipo de dato en la DB) el lector de la DB ["Columna virtual tal cual aparece en commandText"]*/
                    aux.Numero = (int)lector["Numero"]; //2da forma (quiza es mas practico)

                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];

                    //como tipo no va a tener una instancia, porque cuando haga Tipo.Descripcion va a dar referencia nula
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento(); //idem que el anterior
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    lista.Add(aux); //agrega dato a la lista
                }

                conexion.Close();
                return lista; //retorna la lista
            }

            catch (Exception ex) // retorna un error si algo sale mal.
            {

                throw ex;
            }

        }
    }
}
