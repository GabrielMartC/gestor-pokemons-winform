using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejemplos_ado_dotnet
{
    class Pokemon
    {
        public int Numero { get; set; }
        public string Nombre {  get; set; }
        public string Descripcion { get; set; }    
        public string UrlImagen { get; set; }
        public Elemento Tipo { get; set; } //puede ser agua, fuego, etc...
        public Elemento Debilidad { get; set; }



    }
}
