using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejemplos_ado_dotnet
{
    class Elemento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }       /*Sobreescribimos el metodo toString para poder mostrar el Tipo del pokemon,  y no la
                 ruta: ejemplos_ado_dotnet.Elemento*/
    }
}
